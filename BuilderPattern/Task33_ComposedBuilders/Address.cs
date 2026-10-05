namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>Immutable address value. Used for BOTH billing and shipping.</summary>
public sealed class Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    internal Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street; City = city; State = state; ZipCode = zipCode; Country = country;
    }

    public override string ToString() => $"{Street}, {City}, {State} {ZipCode}, {Country}";
}
