using Shop.App.Repositories;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void CreateProduct(string name, decimal price, int stockQuantity)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            StockQuantity = stockQuantity
        };

        _productRepository.Add(product);
        _productRepository.SaveChanges();

        Console.WriteLine($"Продукт '{name}' успішно створено з ID: {product.Id}");
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.GetAll().ToList();
    }

    public void UpdateProductPrice(int productId, decimal newPrice)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
        {
            Console.WriteLine($"Продукт з ID {productId} не знайдено");
            return;
        }

        _productRepository.UpdatePrice(productId, newPrice);
        _productRepository.SaveChanges();

        Console.WriteLine($"Ціну продукту '{product.Name}' оновлено на {newPrice:C}");
    }

    public void DeleteProduct(int productId)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
        {
            Console.WriteLine($"Продукт з ID {productId} не знайдено");
            return;
        }

        _productRepository.Delete(productId);
        _productRepository.SaveChanges();

        Console.WriteLine($"Продукт '{product.Name}' видалено");
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetById(id);
    }
}
