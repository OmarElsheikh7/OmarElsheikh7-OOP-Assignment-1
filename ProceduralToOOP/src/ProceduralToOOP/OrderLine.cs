namespace Part1_ProceduralToOOP;

public class OrderLine
{
    public Product Product { get; }
    public int Quantity { get; }

    public OrderLine(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        if (quantity <= 0)
            throw new DomainException("quantity must be positive.");
        Quantity = quantity;
    }

    public decimal LineTotal => Product.Price * Quantity;

    public override string ToString() =>
        $"{Product.Name} x{Quantity} @{Product.Price:F2} = {LineTotal:F2}";
}
