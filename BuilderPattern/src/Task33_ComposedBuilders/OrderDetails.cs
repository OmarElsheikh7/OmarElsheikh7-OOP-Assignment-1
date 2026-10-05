namespace Part3_BuilderPattern.Task33_ComposedBuilders;

/// <summary>Immutable order/payment info.</summary>
public sealed class OrderDetails
{
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    internal OrderDetails(DateTime orderDate, string paymentMethod, string currency,
                          decimal subTotal, decimal discountAmount, decimal taxAmount)
    {
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = subTotal - discountAmount + taxAmount;   // calculated, never set by hand
    }

    public override string ToString() =>
        $"{OrderDate:yyyy-MM-dd}, {PaymentMethod}, {Currency} | SubTotal {SubTotal:F2} - Discount {DiscountAmount:F2} + Tax {TaxAmount:F2} = TOTAL {TotalAmount:F2}";
}
