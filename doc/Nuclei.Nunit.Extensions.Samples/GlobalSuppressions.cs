//-----------------------------------------------------------------------
// <copyright company="TheNucleus">
// Copyright (c) TheNucleus. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENCE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1062:Validate arguments of public methods",
    MessageId = "0",
    Scope = "member",
    Target = "Nuclei.Nunit.Extensions.Samples.NameObjectTest+NameObjectEqualityContractVerifier.#Copy(Nuclei.Nunit.Extensions.Samples.NameObject)",
    Justification = "This is a sample project. We won't be validating this.")]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Scope = "member",
    Target = "Nuclei.Nunit.Extensions.Samples.NameObjectTest.#RoundTripSerialize()",
    Justification = "This is a test method. There's no point in making it static.")]
