namespace Part1_ProceduralToOOP;

public class Customer
{
    private const decimal VipDiscountRate = 0.10m;

    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string City { get; }
    public bool IsVip { get; }

    public Customer(int id, string name, string email, string city, bool isVip)
    {
        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;
    }

    public decimal ApplyDiscount(decimal amount) =>
        IsVip ? amount * (1 - VipDiscountRate) : amount;

    public override string ToString() =>
        $"#{Id} {Name} <{Email}> {City} vip={(IsVip ? "yes" : "no")}";
}
