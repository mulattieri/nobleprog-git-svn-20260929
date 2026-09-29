namespace Catalog;

public class PriceCalculator
{
    private readonly decimal _vatRate;

    public PriceCalculator(decimal vatRate)
    {
        _vatRate = vatRate;
    }

    public decimal Net(Product product, int quantity)
    {
        return product.UnitPrice * quantity;
    }

    public decimal Gross(Product product, int quantity)
    {
        return Math.Round(Net(product, quantity) * (1 + _vatRate), 2, MidpointRounding.AwayFromZero);
    }

    public decimal Total(IEnumerable<(Product Product, int Quantity)> lines)
    {
        return lines.Sum(l => Gross(l.Product, l.Quantity));
    }
}
