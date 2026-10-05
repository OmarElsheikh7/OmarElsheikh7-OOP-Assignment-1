namespace Part1_ProceduralToOOP;

public class ConsoleMenu
{
    private readonly OrderSystem _system;

    public ConsoleMenu(OrderSystem system) => _system = system;

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_system.Customers.Count}) ===");
        foreach (var c in _system.Customers)
            Console.WriteLine(c);
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_system.Products.Count}) ===");
        foreach (var p in _system.Products)
            Console.WriteLine(p);
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_system.Orders.Count}) ===");
        foreach (var o in _system.Orders)
            Console.WriteLine(o);
    }

    public void PrintSalesTotal(string prefix) =>
        Console.WriteLine($"{prefix}{_system.TotalSalesPaidOnly():F2}");

    private static void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    public void Run()
    {
        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            if (!TryReadInt(out choice))
            {
                if (Console.In.Peek() == -1) return;
                choice = -1;
                Console.WriteLine("Unknown choice.");
                continue;
            }

            try
            {
                Handle(choice);
            }
            catch (DomainException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }

    private void Handle(int choice)
    {
        switch (choice)
        {
            case 1: PrintCustomers(); break;
            case 2: PrintProducts(); break;
            case 3: PrintAllOrders(); break;
            case 4:
                Console.WriteLine(_system.GetOrderForDisplay(ReadInt("Order id: ")));
                break;
            case 5:
                _system.CreateOrder(
                    ReadInt("Order id: "),
                    ReadInt("Customer id: "),
                    ReadString("Date (YYYY-MM-DD): "));
                break;
            case 6:
                _system.AddLineToOrder(
                    ReadInt("Order id: "),
                    ReadInt("Product id: "),
                    ReadInt("Quantity: "));
                break;
            case 7:
                _system.MarkOrderPaid(ReadInt("Order id: "));
                break;
            case 8:
                PrintSalesTotal("Paid sales total: ");
                break;
            case 0:
                Console.WriteLine("Bye.");
                break;
            default:
                Console.WriteLine("Unknown choice.");
                break;
        }
    }

    private static bool TryReadInt(out int value) =>
        int.TryParse(Console.ReadLine()?.Trim(), out value);

    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        if (!TryReadInt(out int value))
            throw new DomainException("invalid number.");
        return value;
    }

    private static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? "";
    }
}
