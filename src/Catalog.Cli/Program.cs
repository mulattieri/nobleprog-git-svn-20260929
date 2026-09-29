using System.Text.Json;
using Catalog;

var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
var settings = JsonDocument.Parse(json).RootElement.GetProperty("Catalog");

var vatRate = settings.GetProperty("VatRate").GetDecimal();
var reducedVatRate = settings.GetProperty("ReducedVatRate").GetDecimal();
var reducedCodes = settings.GetProperty("ReducedVatCodes").EnumerateArray().Select(e => e.GetString()!).ToHashSet();
var dataFile = settings.GetProperty("DataFile").GetString()!;
var currency = settings.GetProperty("Currency").GetString();
var lowStock = settings.GetProperty("LowStockThreshold").GetInt32();

var products = CatalogLoader.Load(dataFile);
var calculator = new PriceCalculator(vatRate, reducedVatRate, reducedCodes);

foreach (var product in products)
{
    Console.WriteLine($"{product.Code,-6} {product.Name,-22} {calculator.Gross(product, 1),10:N2} {currency}{(product.Stock < lowStock ? "  (scorta bassa)" : "")}");
}
