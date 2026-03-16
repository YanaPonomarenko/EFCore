using Microsoft.Extensions.DependencyInjection;
using Shop.App.Services;
using Shop.App.Data;
using Shop.App.Configurators;
using Shop.Domain.Entities;

namespace Shop.App;
//
public class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();
        services.AddDbContext<ShopDbContext>(options =>
        {
            DbContextConfigurators.Configure(options);
        });

        var context = services.BuildServiceProvider()
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<ShopDbContext>();

        if (!context.Database.CanConnect())
        {
            Console.WriteLine("Немає підключення до БД");
            return;
        }

        Console.WriteLine("Підключено до БД\n");

        var category = new Category { Name = "Хліб", CreatedAt = DateTime.Now };
        var product = new Product { Name = "Батон", Price = 22.5m, StockQuantity = 50, CreatedAt = DateTime.Now };

        context.Categories.Add(category);
        context.Products.Add(product);
        context.SaveChanges();

        context.CategoryProducts.Add(new CategoryProduct
        {
            ProductId = product.Id,
            CategoryId = category.Id,
            Store = 16
        });
        context.SaveChanges();

        Console.WriteLine($"Категорія: {category.Name}");
        Console.WriteLine($"Продукт: {product.Name} - {product.Price} грн");

        var link = context.CategoryProducts.FirstOrDefault();
        if (link != null)
            Console.WriteLine($"Зв'язок: {link.Product?.Name} -> {link.Category?.Name}, Магазин {link.Store}");

        Console.WriteLine("\nГотово!");
        Console.ReadKey();
    }
}