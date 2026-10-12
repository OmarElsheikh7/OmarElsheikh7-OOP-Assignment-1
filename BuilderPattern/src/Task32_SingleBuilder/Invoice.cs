namespace Part3_BuilderPattern.Task32_SingleBuilder;

/// <summary>
/// Flat invoice with 21 properties (Task 3.1/3.2).
/// Immutable: the only way to create one is through <see cref="InvoiceBuilder"/>.
/// </summary>
public sealed class Invoice
{
    // Identity / customer
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }

    // Billing address
    public string BillingStreet { get; }
    public string BillingCity { get; }
    public string BillingState { get; }
    public string BillingZipCode { get; }
    public string BillingCountry { get; }

    // Shipping address
    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingState { get; }
    public string ShippingZipCode { get; }
    public string ShippingCountry { get; }

    // Order / payment
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    // Only the builder (same assembly) can call this.
    internal Invoice(
        string invoiceId, string customerName, string customerEmail, string? customerPhone,
        string billingStreet, string billingCity, string billingState, string billingZipCode, string billingCountry,
        string shippingStreet, string shippingCity, string shippingState, string shippingZipCode, string shippingCountry,
        DateTime orderDate, string paymentMethod, string currency,
        decimal subTotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingStreet = billingStreet;
        BillingCity = billingCity;
        BillingState = billingState;
        BillingZipCode = billingZipCode;
        BillingCountry = billingCountry;
        ShippingStreet = shippingStreet;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingZipCode = shippingZipCode;
        ShippingCountry = shippingCountry;
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }

    public override string ToString() =>
$@"Invoice {InvoiceId}  ({OrderDate:yyyy-MM-dd}, {PaymentMethod}, {Currency})
  Customer : {CustomerName} <{CustomerEmail}> {CustomerPhone ?? "(no phone)"}
  Billing  : {BillingStreet}, {BillingCity}, {BillingState} {BillingZipCode}, {BillingCountry}
  Shipping : {ShippingStreet}, {ShippingCity}, {ShippingState} {ShippingZipCode}, {ShippingCountry}
  SubTotal {SubTotal:F2} - Discount {DiscountAmount:F2} + Tax {TaxAmount:F2} = TOTAL {TotalAmount:F2}";
}
