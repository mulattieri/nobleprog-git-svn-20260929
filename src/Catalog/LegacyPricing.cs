namespace Catalog;

// Compatibilità con il vecchio gestionale (listini pre-2020)
public static class LegacyPricing
{
    public const decimal LegacyDiscount = 0.05m;

    public static bool IsLegacyCode(string code) => code.StartsWith("L");
}
