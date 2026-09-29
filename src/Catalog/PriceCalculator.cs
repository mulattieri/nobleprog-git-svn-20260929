namespace Catalog;

public class PriceCalculator
{
    private readonly decimal _vatRate;
    private readonly decimal _reducedVatRate;
    private readonly ISet<string> _reducedCodes;

    public PriceCalculator(decimal vatRate, decimal reducedVatRate = 0m, ISet<string>? reducedCodes = null)
    {
        _vatRate = vatRate;
        _reducedVatRate = reducedVatRate;
        _reducedCodes = reducedCodes ?? new HashSet<string>();
    }

    public decimal VatFor(Product product) =>
        _reducedCodes.Contains(product.Code) ? _reducedVatRate : _vatRate;

    public decimal Net(Product product, int quantity)
    {
        return product.UnitPrice * quantity;
    }

    public decimal Gross(Product product, int quantity)
    {
        return Net(product, quantity) * (1 + VatFor(product));
    }

    public decimal Total(IEnumerable<(Product Product, int Quantity)> lines)
    {
        return lines.Sum(l => Gross(l.Product, l.Quantity));
    }
}
