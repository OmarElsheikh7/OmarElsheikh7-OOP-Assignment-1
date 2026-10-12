namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>
/// Top-level builder: only knows the invoice's own fields and how to COMPOSE the sub-builders.
/// Address and order rules are NOT repeated here.
/// </summary>
public sealed class InvoiceBuilder
{
    private string? _id, _name, _email, _phone;
    private Address? _billing, _shipping;
    private OrderDetails? _order;
    private Exception? _partError;   // first failure from a nested builder

    public InvoiceBuilder WithInvoiceId(string id) { _id = id; return this; }
    public InvoiceBuilder WithCustomer(string name, string email) { _name = name; _email = email; return this; }
    public InvoiceBuilder WithPhone(string phone) { _phone = phone; return this; }

    // Same AddressBuilder, configured inline, for both addresses.
    public InvoiceBuilder WithBilling(Action<AddressBuilder> configure)
    {
        _billing = BuildPart(() => { var b = new AddressBuilder(); configure(b); return b.Build(); }, "Billing");
        return this;
    }

    public InvoiceBuilder WithShipping(Action<AddressBuilder> configure)
    {
        _shipping = BuildPart(() => { var b = new AddressBuilder(); configure(b); return b.Build(); }, "Shipping");
        return this;
    }

    public InvoiceBuilder WithOrder(Action<OrderBuilder> configure)
    {
        _order = BuildPart(() => { var b = new OrderBuilder(); configure(b); return b.Build(); }, "Order");
        return this;
    }

    private T? BuildPart<T>(Func<T> build, string partName) where T : class
    {
        try { return build(); }
        catch (InvalidOperationException ex)
        {
            _partError ??= new InvalidOperationException($"{partName}: {ex.Message}", ex);
            return null;
        }
    }

    public Invoice Build()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(_id)) errors.Add("InvoiceId is required.");
        if (string.IsNullOrWhiteSpace(_name)) errors.Add("CustomerName is required.");
        if (string.IsNullOrWhiteSpace(_email)) errors.Add("CustomerEmail is required.");
        else if (!_email.Contains('@')) errors.Add("CustomerEmail must contain '@'.");

        if (_partError != null) errors.Add(_partError.Message);
        else
        {
            if (_billing is null) errors.Add("Billing address is required.");
            if (_order is null) errors.Add("Order details are required.");
        }

        if (errors.Count > 0)
            throw new InvalidOperationException("Cannot build Invoice:\n  - " + string.Join("\n  - ", errors));

        return new Invoice(_id!, _name!, _email!, _phone, _billing!, _shipping ?? _billing!, _order!);
    }
}
