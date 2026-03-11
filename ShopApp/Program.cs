using Microsoft.Extensions.DependencyInjection;
using Shop.App.Data;
using Shop.App.Configurators;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.App;

public class Program
{
    public static void Main()
    {

        var services = new ServiceCollection(); 
        services.AddDbContext<ShopDbContext>(options =>
        {
            DbContextConfigurators.Configure(options);
        });

        var provider = services.BuildServiceProvider();
 
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
        if (context.Database.CanConnect())
        {
            //var product = new Product
            //{
            //    Name ="булочка",
            //    Price = 39.6m,
            //    CreatedAt = DateTime.Now
            //};
            var productCategory = new CategoryProduct
            {
                ProductId = 4,
                CategoryId = 2,
                Store =16
            };
            context.Add(productCategory);
            context.SaveChanges();




            //var product = new Product
            //{
            //    Name = "батон",
            //    Price = 22.6m,
            //    CreatedAt = DateTime.Now
            //};
            //var category = new Category
            //{
            //    Name ="хлібобулочні вироби",
            //    CreatedAt= DateTime.Now
            //};
            //var productCategory = new CategoryProduct
            //{
            //    Product = product,
            //    CategoryId = 2,
            //    Store =20
            //};
            //context.Add(productCategory);
            //context.SaveChanges();




            //Console.WriteLine("Пiдключення до БД встановлено");
            //User user = new User();
            //user.CreatedAt= DateTime.Now;
            //user.Name = "Bob";
            //user.Surname = "Smith";
            //user.Email = "alex@gmail.com";
            //user.Role = UserRole.ADMIN;
            //user.HashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("qwerty");
            //context.Users.Add(user);
            //context.SaveChanges();
            //string email = "alex@gmail.com";
            //string password = "qwerty2";
            //var user = context.Users.FirstOrDefault(u => u.Email == email);
            //if (user != null)
            //{
            //    if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.HashPassword))
            //    {
            //        Console.WriteLine("Login");
            //    }
            //}

        }
        else
        {
            Console.WriteLine("Не вдалось підключитись до БД");
        }
    }
}
