using Part3_BuilderPattern.Task32_SingleBuilder;
using Part3_BuilderPattern.Task33_ComposedBuilders;
using Single = Part3_BuilderPattern.Task32_SingleBuilder;
using Composed = Part3_BuilderPattern.Task33_ComposedBuilders;

Console.WriteLine("=== Task 3.2: single builder ===");

Single.Invoice a = new Single.InvoiceBuilder()
    .WithInvoiceId("INV-1001")
    .WithCustomer("Mona Ali", "mona@example.com")
    .WithPhone("+20 100 000 0000")
    .WithBillingAddress("12 Nile St", "Cairo", "Cairo", "11511", "Egypt")
    .WithShippingAddress("5 Garden Rd", "Giza", "Giza", "12611", "Egypt")
    .OrderedOn(new DateTime(2026, 9, 15))
    .PaidBy("CreditCard", "EGP")
    .WithSubTotal(1500m)
    .WithDiscount(150m)
    .WithTax(189m)
    .Build();
Console.WriteLine(a);

Console.WriteLine("\n--- missing mandatory fields (expected failure) ---");
try
{
    new Single.InvoiceBuilder()
        .WithInvoiceId("INV-1002")
        .WithCustomer("Omar", "not-an-email")
        .Build();
}
catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }

Console.WriteLine("\n=== Task 3.3: composed builders ===");

Composed.Invoice b = new Composed.InvoiceBuilder()
    .WithInvoiceId("INV-2001")
    .WithCustomer("Mona Ali", "mona@example.com")
    .WithPhone("+20 100 000 0000")
    .WithBilling(x => x.Street("12 Nile St").City("Cairo").State("Cairo").ZipCode("11511").Country("Egypt"))
    .WithShipping(x => x.Street("5 Garden Rd").City("Giza").State("Giza").ZipCode("12611").Country("Egypt"))
    .WithOrder(o => o.OrderedOn(new DateTime(2026, 9, 15)).PaidBy("CreditCard").InCurrency("EGP")
                     .SubTotal(1500m).Discount(150m).Tax(189m))
    .Build();
Console.WriteLine(b);

Console.WriteLine("\n--- shipping omitted: defaults to billing ---");
Composed.Invoice c = new Composed.InvoiceBuilder()
    .WithInvoiceId("INV-2002")
    .WithCustomer("Sara Nabil", "sara@example.com")
    .WithBilling(x => x.Street("7 Pyramids Rd").City("Giza").State("Giza").ZipCode("12556").Country("Egypt"))
    .WithOrder(o => o.OrderedOn(new DateTime(2026, 9, 16)).PaidBy("Cash").InCurrency("EGP").SubTotal(250m))
    .Build();
Console.WriteLine(c);

Console.WriteLine("\n--- incomplete address (AddressBuilder rejects it on its own) ---");
try
{
    new Composed.InvoiceBuilder()
        .WithInvoiceId("INV-2003")
        .WithCustomer("Omar Hassan", "omar@example.com")
        .WithBilling(x => x.Street("1 Main St").City("Alexandria"))   // no state/zip/country
        .WithOrder(o => o.OrderedOn(DateTime.Today).PaidBy("Cash").InCurrency("EGP").SubTotal(100m))
        .Build();
}
catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }

Console.WriteLine("\n--- AddressBuilder used standalone ---");
try { new AddressBuilder().Street("Only street").Build(); }
catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }
