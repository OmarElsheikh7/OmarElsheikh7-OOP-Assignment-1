using Part1_ProceduralToOOP;

Console.WriteLine("Object-Oriented Order System (C#)");
Console.WriteLine("Seed sample data, show a demo, then open the menu.");

var system = new OrderSystem();
var menu = new ConsoleMenu(system);

system.SeedSampleData();
system.RunDemoScenario();

menu.PrintCustomers();
menu.PrintProducts();
menu.PrintAllOrders();
menu.PrintSalesTotal("\nPaid sales total after demo: ");

menu.Run();
