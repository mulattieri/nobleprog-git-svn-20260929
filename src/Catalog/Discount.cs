namespace Catalog;

public record Discount(decimal Percentage, decimal FixedAmount = 0m)
{
    public decimal Apply(decimal price)
    {
        var discounted = price * (1 - Percentage / 100m) - FixedAmount;
        return discounted < 0 ? 0 : discounted;
    }
}
