namespace Part3_BuilderPattern.Task32_SingleBuilder;

/// <summary>
/// Task 3.2 - one fluent builder for the whole flat <see cref="Invoice"/>.
///
/// MANDATORY: InvoiceId, CustomerName, CustomerEmail, billing address (5 fields),
///            OrderDate, PaymentMethod, Currency, SubTotal.
/// OPTIONAL : CustomerPhone, shipping address (defaults to billing), DiscountAmount (0), TaxAmount (0).
/// TotalAmount is never set by hand: it is calculated (SubTotal - Discount + Tax),
/// so it can never disagree with its parts.
///
/// Build() reports EVERY problem at once, so the caller fixes everything in one go.
/// </summary>
public sealed class InvoiceBuilder
{
    private string? _invoiceId, _customerName, _customerEmail, _customerPhone;
    private string? _billStreet, _billCity, _billState, _billZip, _billCountry;
    private string? _shipStreet, _shipCity, _shipState, _shipZip, _shipCountry;
    private DateTime? _orderDate;
    private string? _paymentMethod, _currency;
    private decimal? _subTotal;
    private decimal _discount, _tax;

    // ---- customer ----
    public InvoiceBuilder WithInvoiceId(string id) { _invoiceId = id; return this; }
    public InvoiceBuilder WithCustomer(string name, string email) { _customerName = name; _customerEmail = email; return this; }
    public InvoiceBuilder WithPhone(string phone) { _customerPhone = phone; return this; }

    // ---- billing ----
    public InvoiceBuilder WithBillingAddress(string street, string city, string state, string zip, string country)
    {
        _billStreet = street; _billCity = city; _billState = state; _billZip = zip; _billCountry = country;
        return this;
    }

    // ---- shipping (optional) ----
    public InvoiceBuilder WithShippingAddress(string street, string city, string state, string zip, string country)
    {
        _shipStreet = street; _shipCity = city; _shipState = state; _shipZip = zip; _shipCountry = country;
        return this;
    }

    // ---- order / payment ----
    public InvoiceBuilder OrderedOn(DateTime date) { _orderDate = date; return this; }
    public InvoiceBuilder PaidBy(string method, string currency) { _paymentMethod = method; _currency = currency; return this; }
    public InvoiceBuilder WithSubTotal(decimal subTotal) { _subTotal = subTotal; return this; }
    public InvoiceBuilder WithDiscount(decimal discount) { _discount = discount; return this; }
    public InvoiceBuilder WithTax(decimal tax) { _tax = tax; return this; }

    public Invoice Build()
    {
        var errors = new List<string>();

        void Need(string? value, string name)
        {
            if (string.IsNullOrWhiteSpace(value)) errors.Add($"{name} is required.");
        }

        Need(_invoiceId, "InvoiceId");
        Need(_customerName, "CustomerName");
        Need(_customerEmail, "CustomerEmail");
        if (!string.IsNullOrWhiteSpace(_customerEmail) && !_customerEmail.Contains('@'))
            errors.Add("CustomerEmail must contain '@'.");

        Need(_billStreet, "BillingStreet");
        Need(_billCity, "BillingCity");
        Need(_billState, "BillingState");
        Need(_billZip, "BillingZipCode");
        Need(_billCountry, "BillingCountry");

        if (_orderDate is null) errors.Add("OrderDate is required.");
        Need(_paymentMethod, "PaymentMethod");
        Need(_currency, "Currency");
        if (_subTotal is null) errors.Add("SubTotal is required.");
        else if (_subTotal < 0) errors.Add("SubTotal cannot be negative.");

        if (_discount < 0) errors.Add("DiscountAmount cannot be negative.");
        if (_tax < 0) errors.Add("TaxAmount cannot be negative.");
        if (_subTotal is not null && _discount > _subTotal) errors.Add("DiscountAmount cannot exceed SubTotal.");

        if (errors.Count > 0)
            throw new InvalidOperationException(
                "Cannot build Invoice:\n  - " + string.Join("\n  - ", errors));

        // Shipping defaults to billing when not provided.
        bool hasShipping = _shipStreet != null;
        decimal total = _subTotal!.Value - _discount + _tax;

        return new Invoice(
            _invoiceId!, _customerName!, _customerEmail!, _customerPhone,
            _billStreet!, _billCity!, _billState!, _billZip!, _billCountry!,
            hasShipping ? _shipStreet! : _billStreet!,
            hasShipping ? _shipCity! : _billCity!,
            hasShipping ? _shipState! : _billState!,
            hasShipping ? _shipZip! : _billZip!,
            hasShipping ? _shipCountry! : _billCountry!,
            _orderDate!.Value, _paymentMethod!, _currency!,
            _subTotal.Value, _discount, _tax, total);
    }
}
