namespace Part1_ProceduralToOOP;

public class Order
{
    public const int MaxLines = 20;

    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;

    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Date = date;
    }
    public void AddLine(Product product, int quantity)
    {
        if (IsPaid)
            throw new DomainException("cannot change a paid order.");
        if (_lines.Count >= MaxLines)
            throw new DomainException("order has too many lines.");
        if (quantity <= 0)
            throw new DomainException("quantity must be positive.");
        if (!product.HasStock(quantity))
            throw new DomainException($"not enough stock for product #{product.Id}.");

        product.RemoveStock(quantity);
        _lines.Add(new OrderLine(product, quantity));
    }

    public void MarkPaid()
    {
        if (_lines.Count == 0)
            throw new DomainException("cannot pay an empty order.");
        IsPaid = true;
    }

    public decimal Total => Customer.ApplyDiscount(_lines.Sum(l => l.LineTotal));

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"\n=== ORDER #{Id} ===");
        sb.AppendLine($"Date: {Date}");
        sb.AppendLine($"Customer: {Customer.Name} (#{Customer.Id})");
        sb.AppendLine($"Paid: {(IsPaid ? "yes" : "no")}");
        sb.AppendLine("Lines:");
        foreach (var line in _lines)
            sb.AppendLine($"  - {line}");
        sb.Append($"TOTAL: {Total:F2}");
        return sb.ToString();
    }
}
