using Microsoft.EntityFrameworkCore;
using Shop.App.Data;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Services;
//
public class OrderServices
{
    private readonly ShopDbContext _context;

    public OrderServices(ShopDbContext context)
    {
        _context = context;
    }
    //06.03
    public void CreateOrder(int userId, List<(int productId, int quantity)> items)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productId);
                if (product == null)
                {
                    Console.WriteLine($"Продукт з ID {item.productId} не знайдено");
                    return;
                }

                if (product.StockQuantity < item.quantity)
                {
                    Console.WriteLine($"Недостатньо {product.Name} на складі. Доступно: {product.StockQuantity}");
                    return;
                }
            }
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Нове",
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productId);

                var orderItem = new OrderItem
                {
                    ProductId = item.productId,
                    Quantity = item.quantity,
                    Price = product.Price,
                    Order = order
                };

                order.OrderItems.Add(orderItem);
                totalAmount += product.Price * item.quantity;
                product.StockQuantity -= item.quantity;
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Add(order);
            _context.SaveChanges();
            transaction.Commit();

            Console.WriteLine($"Замовлення {order.Id} створено для користувача {userId}");
            Console.WriteLine($"Сума: {totalAmount}");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public void PlaceOrder(int userId, List<(int productId, int quantity)> items)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                Console.WriteLine($"Користувача з ID {userId} не знайдено");
                return;
            }
            //06.03 hw
            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productId);
                if (product == null)
                {
                    Console.WriteLine($"Продукт з ID {item.productId} не знайдено");
                    return;
                }

                if (product.StockQuantity < item.quantity)
                {
                    Console.WriteLine($"Недостатньо {product.Name} на складі. Доступно: {product.StockQuantity}");
                    return; 
                }
            }
                var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Нове",
                OrderItems = new List<OrderItem>()
            };
            decimal totalAmount = 0;

            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productId);
                if (product == null)
                {
                    Console.WriteLine($"Продукт з ID {item.productId} не знайдено");
                    return;
                }

                if (product.StockQuantity < item.quantity)
                {
                    Console.WriteLine($"Недостатньо {product.Name} на складі. Доступно: {product.StockQuantity}");
                    return;
                }
                var orderItem = new OrderItem
                {
                    ProductId = item.productId,
                    Quantity = item.quantity,
                    Price = product.Price,
                    Order = order
                };

                order.OrderItems.Add(orderItem);
                totalAmount += product.Price * item.quantity;
                product.StockQuantity -= item.quantity;

            }

            order.TotalAmount = totalAmount;
            _context.Orders.Add(order);
            _context.SaveChanges();
            transaction.Commit();

            Console.WriteLine($"Замовлення {order.Id} створено для {user.Name} {user.Surname}");
            Console.WriteLine($"Сума: {totalAmount}");

        }
        catch (Exception ex)
        {
            transaction.Rollback(); 
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public void ShowUserOrders(int userId)
    {
        var user = _context.Users.Find(userId);
        if (user == null)
        {
            Console.WriteLine($"Користувача з ID {userId} не знайдено");
            return;
        }
        var orders = _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        if (!orders.Any())
        {
            Console.WriteLine($"У {user.Name} {user.Surname} немає замовлень");
            return;
        }

        Console.WriteLine($"\nЗамовлення: {user.Name} {user.Surname}");
        foreach (var order in orders)
        {
            Console.WriteLine($"\nЗамовлення {order.Id} від {order.OrderDate:dd.MM.yyyy}");
            Console.WriteLine($"Статус: {order.Status}");
            Console.WriteLine($"Сума: {order.TotalAmount}");
            Console.WriteLine("Товари:");

            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"{item.Product?.Name} x{item.Quantity} = {item.Price * item.Quantity:C}");
            }
        }
    }
}
