//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Nuclei.Nunit.Extensions.Samples
{
    /// <summary>
    /// Defines the tests for the <see cref="NameObject"/> class.
    /// </summary>
    [TestFixture]
    public sealed class NameObjectTest : EqualityContractVerifierTest
    {
        private sealed class NameObjectEqualityContractVerifier : EqualityContractVerifier<NameObject>
        {
            private readonly NameObject _first = new NameObject("a");

            private readonly NameObject _second = new NameObject("b");

            /// <summary>
            /// Creates a deep copy of the given object.
            /// </summary>
            /// <remarks>
            /// This method is used to create identical objects that are not referentially identical.
            /// </remarks>
            /// <param name="original">The original object that should be copied.</param>
            /// <returns>A new instance that contains the same values as the original object.</returns>
            protected override NameObject Copy(NameObject original)
            {
                return new NameObject(original.Name);
            }

            /// <summary>
            /// Gets the first object that should be used in equality comparisons.
            /// </summary>
            protected override NameObject FirstInstance
            {
                get
                {
                    return _first;
                }
            }

            /// <summary>
            /// Gets the second object that should be used in equality comparisons.
            /// </summary>
            protected override NameObject SecondInstance
            {
                get
                {
                    return _second;
                }
            }

            /// <summary>
            /// Gets a value indicating whether operator overloads are defined for the type.
            /// </summary>
            protected override bool HasOperatorOverloads
            {
                get
                {
                    return true;
                }
            }
        }

        private sealed class NameObjectHashCodeContractVerfier : HashCodeContractVerifier
        {
            private readonly IEnumerable<NameObject> _distinctInstances
                = new List<NameObject>
                     {
                        new NameObject("a"),
                        new NameObject("b"),
                        new NameObject("c"),
                        new NameObject("d"),
                        new NameObject("e"),
                        new NameObject("f"),
                        new NameObject("g"),
                        new NameObject("h"),
                     };

            protected override IEnumerable<int> GetHashCodes()
            {
                return _distinctInstances.Select(i => i.GetHashCode());
            }
        }

        private readonly NameObjectHashCodeContractVerfier _hashCodeVerifier
            = new NameObjectHashCodeContractVerfier();

        private readonly NameObjectEqualityContractVerifier _equalityVerifier
            = new NameObjectEqualityContractVerifier();

        /// <summary>
        /// Gets an instance of the hashcode contract verifier for use in the tests.
        /// </summary>
        protected override HashCodeContractVerifier HashContract
        {
            get
            {
                return _hashCodeVerifier;
            }
        }

        /// <summary>
        /// Gets the object that provides the objects to be compared.
        /// </summary>
        protected override IEqualityContractVerifier EqualityContract
        {
            get
            {
                return _equalityVerifier;
            }
        }
    }
}
