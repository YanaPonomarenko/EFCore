using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.App.Configurators;
using Shop.App.Data;
using Shop.App.Managers;
using Shop.App.Repositories;
using Shop.App.Services;
using Shop.Domain.Entities;

namespace Shop.App;
// 09.03
class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer("Server=QQ12\\SQLEXPRESS;Database=ShopDb;Trusted_Connection=True;"));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ShopManager>();

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
            context.Database.EnsureCreated();
        }

        var shopManager = serviceProvider.GetService<ShopManager>();
        shopManager.Run();
    }
}

//
//public class Program
//{
//    public static void Main()
//    {
//        var services = new ServiceCollection();
//        services.AddDbContext<ShopDbContext>(options =>
//        {
//            DbContextConfigurators.Configure(options);
//        });

//        var context = services.BuildServiceProvider()
//            .CreateScope()
//            .ServiceProvider
//            .GetRequiredService<ShopDbContext>();

//        if (!context.Database.CanConnect())
//        {
//            Console.WriteLine("Немає підключення до БД");
//            return;
//        }

//        Console.WriteLine("Підключено до БД\n");

//        var category = new Category { Name = "Хліб", CreatedAt = DateTime.Now };
//        var product = new Product { Name = "Батон", Price = 22.5m, StockQuantity = 50, CreatedAt = DateTime.Now };

//        context.Categories.Add(category);
//        context.Products.Add(product);
//        context.SaveChanges();

//        context.CategoryProducts.Add(new CategoryProduct
//        {
//            ProductId = product.Id,
//            CategoryId = category.Id,
//            Store = 16
//        });
//        context.SaveChanges();

//        Console.WriteLine($"Категорія: {category.Name}");
//        Console.WriteLine($"Продукт: {product.Name} - {product.Price} грн");

//        var link = context.CategoryProducts.FirstOrDefault();
//        if (link != null)
//            Console.WriteLine($"Зв'язок: {link.Product?.Name} -> {link.Category?.Name}, Магазин {link.Store}");

//        Console.WriteLine("\nГотово!");
//        Console.ReadKey();
//    }
//}