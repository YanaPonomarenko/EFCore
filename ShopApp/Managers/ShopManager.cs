using Shop.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Managers;

public class ShopManager
{
    private readonly IProductService _productService;
    private readonly ProductTestService _testService;

    public ShopManager(IProductService productService, ProductTestService testService)  // ЗМІНИ КОНСТРУКТОР
    {
        _productService = productService;
        _testService = testService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nМагазин ");
            Console.WriteLine("1 - Всі продукти");
            Console.WriteLine("2 - Створити продукт");
            Console.WriteLine("3 - Оновити ціну");
            Console.WriteLine("4 - Видалити продукт");
            Console.WriteLine("5 - Продукт за ID");
            Console.WriteLine("6 - Тестувати швидкість пошуку");
            Console.WriteLine("7 - Заповнити тестовими даними");
            Console.WriteLine("0 - Вихід");
            Console.Write("Вибір: ");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 0)
            {
                break;
            }

            switch (choice)
            {
                case 1:
                    ShowAllProducts();
                    break;
                case 2:
                    CreateProduct();
                    break;
                case 3:
                    UpdateProductPrice();
                    break;
                case 4:
                    DeleteProduct();
                    break;
                case 5:
                    ShowProductById();
                    break;
                case 6: 
                    _testService.TestSearchPerformance();
                    break;
                case 7: 
                    Console.Write("Скільки продуктів згенерувати? ");
                    int count = int.Parse(Console.ReadLine());
                    _testService.SeedProducts(count);
                    break;
                case 0:
                    Console.WriteLine("Кінець програми");
                    break;
                default:
                    Console.WriteLine("Невірний вибір");
                    break;
            }
        }
    }

    private void ShowAllProducts()
    {
        var products = _productService.GetAllProducts();

        if (products.Count() == 0)
        {
            Console.WriteLine("Немає продуктів");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}: {product.Name} - {product.Price} грн ({product.StockQuantity} шт.)");
        }
    }

    private void CreateProduct()
    {
        Console.Write("Назва продукту: ");
        string name = Console.ReadLine();

        Console.Write("Ціна продукту: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Кількість на складі: ");
        int stock = int.Parse(Console.ReadLine());

        _productService.CreateProduct(name, price, stock);
    }

    private void UpdateProductPrice()
    {
        Console.Write("ID продукту: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Нова ціна: ");
        decimal price = decimal.Parse(Console.ReadLine());

        _productService.UpdateProductPrice(id, price);
    }

    private void DeleteProduct()
    {
        Console.Write("ID продукту: ");
        int id = int.Parse(Console.ReadLine());

        _productService.DeleteProduct(id);
    }

    private void ShowProductById()
    {
        Console.Write("ID продукту: ");
        int id = int.Parse(Console.ReadLine());

        var product = _productService.GetProductById(id);

        if (product == null)
        {
            Console.WriteLine("Продукт не знайдено");
            return;
        }

        Console.WriteLine($"{product.Id}: {product.Name} - {product.Price} грн ({product.StockQuantity} шт.)");
    }
}
