//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Nuclei.Nunit.Extensions
{
    /// <summary>
    /// Defines a chi-squared test.
    /// </summary>
    /// <remarks>
    /// This code is based on, but not exactly the same as, the code of the hashcode contract verifier in the MbUnit
    /// project which is licensed under the Apache License 2.0. More information can be found at:
    /// https://code.google.com/p/mb-unit/.
    /// </remarks>
    internal sealed class ChiSquareTest
    {
        // Fields
        private readonly double _chiSquareValue;

        private readonly int _degreesOfFreedom;

        private readonly double _twoTailedpValue;

        // Methods
        public ChiSquareTest(double expected, ICollection<double> actual, int numberOfConstraints)
        {
            if (expected <= 0.0)
            {
                throw new ArgumentOutOfRangeException("expected", "The expected value is negative.");
            }

            if (actual == null)
            {
                throw new ArgumentNullException("actual");
            }

            _degreesOfFreedom = actual.Count - numberOfConstraints;
            foreach (double num in actual)
            {
                double num2 = num - expected;
                _chiSquareValue += (num2 * num2) / expected;
            }

            _twoTailedpValue = Gamma.IncompleteGamma(_degreesOfFreedom / 2.0, _chiSquareValue / 2.0);
        }

        public double TwoTailedpValue
        {
            get
            {
                return _twoTailedpValue;
            }
        }
    }
}
