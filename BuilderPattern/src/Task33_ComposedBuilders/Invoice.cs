namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>Invoice composed of small parts: customer info + 2 Addresses + OrderDetails.</summary>
public sealed class Invoice
{
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }
    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }
    public OrderDetails Order { get; }

    internal Invoice(string invoiceId, string customerName, string customerEmail, string? customerPhone,
                     Address billing, Address shipping, OrderDetails order)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingAddress = billing;
        ShippingAddress = shipping;
        Order = order;
    }

    public override string ToString() =>
$@"Invoice {InvoiceId}
  Customer : {CustomerName} <{CustomerEmail}> {CustomerPhone ?? "(no phone)"}
  Billing  : {BillingAddress}
  Shipping : {ShippingAddress}
  Order    : {Order}";
}
