//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Nuclei.Nunit.Extensions
{
    /// <summary>
    /// Stores a number of hashcodes for use in collision calculations.
    /// </summary>
    /// <remarks>
    /// This code is based on, but not exactly the same as, the code of the hashcode contract verifier in the MbUnit
    /// project which is licensed under the Apache License 2.0. More information can be found at:
    /// https://code.google.com/p/mb-unit/.
    /// </remarks>
    internal sealed class HashStore
    {
        private readonly IDictionary<int, int> _more = new Dictionary<int, int>();

        private readonly HashSet<int> _one = new HashSet<int>();

        private readonly HashStoreResult _result;

        private readonly HashSet<int> _two = new HashSet<int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HashStore"/> class.
        /// </summary>
        /// <param name="hashes">The collection of hashcodes.</param>
        public HashStore(IEnumerable<int> hashes)
        {
            int actual = 0;
            foreach (int num2 in hashes)
            {
                Add(num2);
                actual++;
            }

            if (actual < 2)
            {
                throw new NotEnoughHashesException(2, actual);
            }

            _result = CalculateResults(actual);
        }

        private void Add(int hash)
        {
            if (_one.Contains(hash))
            {
                _one.Remove(hash);
                _two.Add(hash);
            }
            else if (_two.Contains(hash))
            {
                _two.Remove(hash);
                _more.Add(hash, 3);
            }
            else
            {
                int num;
                if (_more.TryGetValue(hash, out num))
                {
                    _more[hash] = 1 + num;
                }
                else
                {
                    _one.Add(hash);
                }
            }
        }

        private HashStoreResult CalculateResults(int count)
        {
            int bucketSize = GetBucketSize();
            var actual = new double[bucketSize];
            double collisionProbability = 0.0;
            int num3 = 0;
            for (int i = 0; i < _one.Count; i++)
            {
                actual[num3++ % bucketSize]++;
            }

            for (int j = 0; j < _two.Count; j++)
            {
                actual[num3++ % bucketSize] += 2.0;
                collisionProbability += 2.0 / (count * (count - 1));
            }

            foreach (KeyValuePair<int, int> pair in _more)
            {
                actual[num3++ % bucketSize] += pair.Value;
                collisionProbability += ((pair.Value / ((double)count)) * (pair.Value - 1)) / (count - 1);
            }

            var test = new ChiSquareTest(count / ((double)bucketSize), actual, 1);
            return new HashStoreResult(collisionProbability, 1.0 - test.TwoTailedpValue);
        }

        private int GetBucketSize()
        {
            var numArray = new[] { 0x39dd, 0xe1d, 0xdf, 0x11 };
            int num = (_one.Count + this._two.Count) + _more.Count;
            foreach (int num2 in numArray)
            {
                if (num >= (num2 * 10))
                {
                    return num2;
                }
            }

            return num;
        }

        public HashStoreResult Result
        {
            get
            {
                return _result;
            }
        }
    }
}
