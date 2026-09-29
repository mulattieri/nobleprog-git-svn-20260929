using System.Globalization;

namespace Catalog;

public static class CatalogLoader
{
    public static List<Product> Load(string path)
    {
        return File.ReadLines(path)
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(';'))
            .Select(f => new Product(f[0], f[1], decimal.Parse(f[2], CultureInfo.InvariantCulture), int.Parse(f[3])))
            .ToList();
    }
}
