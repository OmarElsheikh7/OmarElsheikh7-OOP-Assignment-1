namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>
/// Owns ONLY address rules. All five parts are mandatory, so a built Address is always complete -
/// the Invoice never needs to know what a valid street/city/zip looks like.
/// </summary>
public sealed class AddressBuilder
{
    private string? _street, _city, _state, _zip, _country;

    public AddressBuilder Street(string v) { _street = v; return this; }
    public AddressBuilder City(string v) { _city = v; return this; }
    public AddressBuilder State(string v) { _state = v; return this; }
    public AddressBuilder ZipCode(string v) { _zip = v; return this; }
    public AddressBuilder Country(string v) { _country = v; return this; }

    public Address Build()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(_street)) errors.Add("Street is required.");
        if (string.IsNullOrWhiteSpace(_city)) errors.Add("City is required.");
        if (string.IsNullOrWhiteSpace(_state)) errors.Add("State is required.");
        if (string.IsNullOrWhiteSpace(_zip)) errors.Add("ZipCode is required.");
        if (string.IsNullOrWhiteSpace(_country)) errors.Add("Country is required.");

        if (errors.Count > 0)
            throw new InvalidOperationException("Invalid address: " + string.Join(" ", errors));

        return new Address(_street!, _city!, _state!, _zip!, _country!);
    }
}
