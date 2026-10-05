namespace Part1_ProceduralToOOP;

public class OrderSystem
{
    public const int MaxCustomers = 50;
    public const int MaxProducts = 50;
    public const int MaxOrders = 100;

    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Customer> Customers => _customers;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Order> Orders => _orders;

    // ---------- Customers ----------
    public void AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (_customers.Count >= MaxCustomers)
            throw new DomainException("customer list is full.");
        if (FindCustomer(id) != null)
            throw new DomainException($"customer id {id} already exists.");

        _customers.Add(new Customer(id, name, email, city, isVip));
    }

    public Customer? FindCustomer(int id) => _customers.FirstOrDefault(c => c.Id == id);

    // ---------- Products ----------
    public void AddProduct(int id, string name, decimal price, int stock)
    {
        if (_products.Count >= MaxProducts)
            throw new DomainException("product list is full.");
        if (FindProduct(id) != null)
            throw new DomainException($"product id {id} already exists.");

        _products.Add(new Product(id, name, price, stock));
    }

    public Product? FindProduct(int id) => _products.FirstOrDefault(p => p.Id == id);

    // ---------- Orders ----------
    public Order CreateOrder(int orderId, int customerId, string date)
    {
        if (_orders.Count >= MaxOrders)
            throw new DomainException("order list is full.");
        if (FindOrder(orderId) != null)
            throw new DomainException($"order id {orderId} already exists.");

        var customer = FindCustomer(customerId)
            ?? throw new DomainException($"customer id {customerId} not found.");

        var order = new Order(orderId, customer, date);
        _orders.Add(order);
        return order;
    }

    public Order? FindOrder(int id) => _orders.FirstOrDefault(o => o.Id == id);

    private Order GetOrder(int id) =>
        FindOrder(id) ?? throw new DomainException($"order id {id} not found.");

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        var order = GetOrder(orderId);
        var product = FindProduct(productId)
            ?? throw new DomainException($"product id {productId} not found.");

        order.AddLine(product, quantity);
    }

    public void MarkOrderPaid(int orderId) => GetOrder(orderId).MarkPaid();

    public Order GetOrderForDisplay(int orderId) => GetOrder(orderId);

    public decimal TotalSalesPaidOnly() => _orders.Where(o => o.IsPaid).Sum(o => o.Total);

    // ---------- Seed / demo ----------
    public void SeedSampleData()
    {
        AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
        AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
        AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

        AddProduct(101, "USB Cable", 50.0m, 100);
        AddProduct(102, "Wireless Mouse", 250.0m, 40);
        AddProduct(103, "Mechanical Keyboard", 1200.0m, 15);
        AddProduct(104, "Laptop Stand", 400.0m, 25);
    }

    public void RunDemoScenario()
    {
        CreateOrder(1001, 1, "2026-09-15");
        AddLineToOrder(1001, 101, 2);
        AddLineToOrder(1001, 102, 1);
        MarkOrderPaid(1001);

        CreateOrder(1002, 2, "2026-09-15");
        AddLineToOrder(1002, 103, 1);
        AddLineToOrder(1002, 104, 1);

        CreateOrder(1003, 3, "2026-09-16");
        AddLineToOrder(1003, 101, 5);
        MarkOrderPaid(1003);
    }
}
