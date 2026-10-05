namespace Part1_ProceduralToOOP;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public bool HasStock(int quantity) => Stock >= quantity;

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("quantity must be positive.");
        if (!HasStock(quantity))
            throw new DomainException($"not enough stock for product #{Id}.");

        Stock -= quantity;
    }

    public override string ToString() => $"#{Id} {Name} price={Price:F2} stock={Stock}";
}
