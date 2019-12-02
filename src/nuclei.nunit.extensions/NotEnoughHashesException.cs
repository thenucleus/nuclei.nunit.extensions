//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Nuclei.Nunit.Extensions
{
    /// <summary>
    /// An exception thrown if not enough hashcodes are provided to the <see cref="HashCodeContractVerifier"/>.
    /// </summary>
    /// <remarks>
    /// This code is based on, but not exactly the same as, the code of the hashcode contract verifier in the MbUnit
    /// project which is licensed under the Apache License 2.0. More information can be found at:
    /// https://code.google.com/p/mb-unit/.
    /// </remarks>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Unit tests do not need documentation.")]
    public sealed class NotEnoughHashesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughHashesException"/> class.
        /// </summary>
        public NotEnoughHashesException()
            : this("Not enough hashes.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughHashesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NotEnoughHashesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughHashesException"/> class.
        /// </summary>
        /// <param name="expected">The expected number of hashes.</param>
        /// <param name="actual">The actual number of hashes.</param>
        public NotEnoughHashesException(int expected, int actual)
            : this(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Expected at least {0} hashes. Got {1} hashes.",
                    expected,
                    actual))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughHashesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NotEnoughHashesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
