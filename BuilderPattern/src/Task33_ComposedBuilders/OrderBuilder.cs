namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>
/// Owns ONLY order/payment rules.
/// Mandatory: OrderDate, PaymentMethod, Currency, SubTotal. Optional: Discount, Tax (default 0).
/// </summary>
public sealed class OrderBuilder
{
    private DateTime? _date;
    private string? _method, _currency;
    private decimal? _subTotal;
    private decimal _discount, _tax;

    public OrderBuilder OrderedOn(DateTime date) { _date = date; return this; }
    public OrderBuilder PaidBy(string method) { _method = method; return this; }
    public OrderBuilder InCurrency(string currency) { _currency = currency; return this; }
    public OrderBuilder SubTotal(decimal v) { _subTotal = v; return this; }
    public OrderBuilder Discount(decimal v) { _discount = v; return this; }
    public OrderBuilder Tax(decimal v) { _tax = v; return this; }

    public OrderDetails Build()
    {
        var errors = new List<string>();
        if (_date is null) errors.Add("OrderDate is required.");
        if (string.IsNullOrWhiteSpace(_method)) errors.Add("PaymentMethod is required.");
        if (string.IsNullOrWhiteSpace(_currency)) errors.Add("Currency is required.");
        if (_subTotal is null) errors.Add("SubTotal is required.");
        else if (_subTotal < 0) errors.Add("SubTotal cannot be negative.");
        if (_discount < 0) errors.Add("DiscountAmount cannot be negative.");
        if (_tax < 0) errors.Add("TaxAmount cannot be negative.");
        if (_subTotal is not null && _discount > _subTotal) errors.Add("DiscountAmount cannot exceed SubTotal.");

        if (errors.Count > 0)
            throw new InvalidOperationException("Invalid order details: " + string.Join(" ", errors));

        return new OrderDetails(_date!.Value, _method!, _currency!, _subTotal!.Value, _discount, _tax);
    }
}
