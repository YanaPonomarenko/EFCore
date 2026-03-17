using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Data;

public static class ProductSeeders
{
    private static readonly Random _random = new Random();
    private static readonly string[] _prefixes = { "Super", "Mega", "Ultra", "Pro", "Premium", "Gold", "Smart", "Cool", "Best", "Top" };
    private static readonly string[] _names = { "Phone", "Laptop", "Tablet", "Monitor", "Keyboard", "Mouse", "Headphones", "Speaker", "Camera", "Printer" };
    private static readonly string[] _suffixes = { "X", "Z", "Plus", "Max", "Pro", "Lite", "Air", "Mini", "Ultra", "2024" };

    public static List<Product> GenerateProducts(int count)
    {
        var products = new List<Product>();

        for (int i = 1; i <= count; i++)
        {
            var product = new Product
            {
                Name = GenerateProductName(),
                Price = _random.Next(100, 10000) + _random.Next(0, 99) / 100m,
                StockQuantity = _random.Next(0, 1000)
            };
            products.Add(product);
        }

        return products;
    }

    private static string GenerateProductName()
    {
        string prefix = _prefixes[_random.Next(_prefixes.Length)];
        string name = _names[_random.Next(_names.Length)];
        string suffix = _suffixes[_random.Next(_suffixes.Length)];

        return $"{prefix} {name} {suffix}";
    }
}
