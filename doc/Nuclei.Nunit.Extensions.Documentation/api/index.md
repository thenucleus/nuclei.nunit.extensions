
# Nuclei.Nunit.Extensions

The following class will be used in the samples below:

[!code-csharp[ExampleObject](..\..\Nuclei.Nunit.Extensions.Samples\NameObject.cs?range=8-)]


## Testing serialization

To test the ability to serialize and deserialize without losing information you can use the `RoundTripSerialize` method on the `AssertExtensions` class.
This method will serialize an object instance and then deserialize it, returning a new instance. This instance can then be compared to the original.

[!code-csharp[RoundTripTest](..\..\Nuclei.Nunit.Extensions.Samples\NameObjectTest.cs?range=125-132)]

## Testing GetHashCode implementations

To verify that the `GetHashCode` method is implemented correctly create a test that derives from the `HashCodeContractVerifierTest` class and provide
an implementation of the `HashCodeContractVerifier` class.

[!code-csharp[HashCodeContractVerification](..\..\Nuclei.Nunit.Extensions.Samples\NameObjectTest.cs?range=72-92,93-95,99-109)]

The test will verify that:

* The collison probability is less than 1%
* The distribution of the hash codes is uniform

## Testing equality implementations

To verify that the equality methods (`object.Equals`, `IEquatable<T>.Equals`) and operators are implemented correctly create a test that derives from the
`EqualityContractVerifierTest` class and provide an implementation of the `EqualityContractVerifier` class.

[!code-csharp[EqualityContractVerification](..\..\Nuclei.Nunit.Extensions.Samples\NameObjectTest.cs?range=20-71,96-98,110-120)]

The test will verify that:

* `object.Equals` is implemented correctly:
  * An instance and it's deep clone are equal
  * An instance is not equal to `null`
  * An instance is not equal to an instance with different values
  * An instance is not equal to an instance of a different type
* If the class implements `IEquatable<T>.Equals` that it is implemented correctly
  * An instance and it's deep clone are equal
  * An instance is not equal to an instance with different values
* If the equality operators (`==` and `!=`) are overloaded that they are implemented correctly
  * The equals operator returns `true` when comparing an instance and it's deep clone
  * The equals operator returns `false` when comparing an instance and an instance with different values
  * The equals operator returns `false` when comparing an instance to `null`
  * The equals operator returns `false` when comparing `null` to an instance
  * The inequals operator returns `false` when comparing an instance and it's deep clone
  * The inequals operator returns `true` when comparing an instance and an instance with different values
  * The inequals operator returns `true` when comparing an instance to `null`
  * The inequals operator returns `true` when comparing `null` to an instance
* The hash codes for two equal instances are the same

Note that deriving from the `EqualityContractVerifierTest` also implies deriving from the `HashCodeContractVerifierTest`.
