//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NUnit.Framework;

namespace Nuclei.Nunit.Extensions
{
    /// <summary>
    /// Defines the base class for tests that verify that exceptions are correctly implemented.
    /// </summary>
    /// <typeparam name="TException">The type of the exception that is being tested.</typeparam>
    /// <remarks>
    /// This code is based on, but not exactly the same as, the code of the exception contract verifier in the MbUnit
    /// project which is licensed under the Apache License 2.0. More information can be found at:
    /// https://code.google.com/p/mb-unit/.
    /// </remarks>
    public abstract class ExceptionContractVerifier<TException>
        where TException : Exception
    {
        /// <summary>
        /// Verifies that the exception has a parameterless constructor.
        /// </summary>
        [Test]
        public void HasDefaultConstructor()
        {
            var constructor = typeof(TException).GetConstructor(Array.Empty<Type>());
            Assert.NotNull(constructor);
        }

        /// <summary>
        /// Verifies that the exception has a constructor that takes an exception message.
        /// </summary>
        [Test]
        public void HasMessageConstructor()
        {
            var constructor = typeof(TException).GetConstructor(
                new[]
                    {
                        typeof(string),
                    });
            Assert.NotNull(constructor);

            var text = "a";
            var instance = (TException)constructor.Invoke(new object[] { text });
            Assert.AreEqual(text, instance.Message);
        }

        /// <summary>
        /// Verifies that the exception has a constructor that takes both an inner exception and a message.
        /// </summary>
        [Test]
        [SuppressMessage(
            "Microsoft.Usage",
            "CA2201:DoNotRaiseReservedExceptionTypes",
            Justification = "Not actually throwing the exception. We just need an exception to store.")]
        public void HasMessageAndExceptionConstructor()
        {
            var constructor = typeof(TException).GetConstructor(
                new[]
                    {
                        typeof(string),
                        typeof(Exception),
                    });
            Assert.NotNull(constructor);

            var text = "a";
            var inner = new Exception();
            var instance = (TException)constructor.Invoke(
                new object[]
                {
                    text,
                    inner,
                });
            Assert.AreEqual(text, instance.Message);
            Assert.AreSame(inner, instance.InnerException);
        }
    }
}
