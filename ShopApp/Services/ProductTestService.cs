using Shop.App.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Services;

public class ProductTestService
{
    private readonly ShopDbContext _context;

    public ProductTestService(ShopDbContext context)
    {
        _context = context;
    }

    public void SeedProducts(int count)
    {
        Console.WriteLine($"\nГенераця {count} продуктів");

        var products = ProductSeeders.GenerateProducts(count);

        _context.Products.AddRange(products);
        _context.SaveChanges();

        Console.WriteLine($"Додано {count} продуктів в базу даних");
    }

    public void TestSearchPerformance()
    {
        Console.WriteLine("\nШвидкість пошуку");
        Console.Write("Введіть назву продукту для пошуку: ");
        string searchName = Console.ReadLine();

        Console.WriteLine("\n1. Пошук БЕЗ індексу:");
        long timeWithoutIndex = MeasureSearchTime(searchName);

        Console.WriteLine("\n2. Створення індекс");
        CreateIndex();

        Console.WriteLine("\n3. Пошук З індексом:");
        long timeWithIndex = MeasureSearchTime(searchName);

        Console.WriteLine("\nРезультат");
        Console.WriteLine($"Без індексу: {timeWithoutIndex} мс");
        Console.WriteLine($"З індексом:  {timeWithIndex} мс");

        if (timeWithIndex < timeWithoutIndex)
        {
            double faster = (double)timeWithoutIndex / timeWithIndex;
            Console.WriteLine($"Індекс пришвидшив пошук у {faster:F1} разів!");
        }

    }

    private long MeasureSearchTime(string searchName)
    {
        _context.ChangeTracker.Clear();

        var stopwatch = Stopwatch.StartNew();

        var product = _context.Products
            .FirstOrDefault(p => p.Name == searchName);

        stopwatch.Stop();

        if (product != null)
            Console.WriteLine($"Знайдено: {product.Name}");
        else
            Console.WriteLine("Продукт не знайдено");

        Console.WriteLine($"Час: {stopwatch.ElapsedMilliseconds} мс");

        return stopwatch.ElapsedMilliseconds;
    }

    private void CreateIndex()
    {
        try
        {
            _context.Database.ExecuteSqlRaw(
                "CREATE INDEX IX_Products_Name ON Products(Name)");
            Console.WriteLine("Індекс створено!");
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("already exists"))
            {
                Console.WriteLine("Індекс вже існує");
            }
            else
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}