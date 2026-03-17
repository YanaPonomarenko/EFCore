using Shop.App.Data;
using Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _context;

    public ProductRepository(ShopDbContext context)
    {
        _context = context;
    }

    public Product GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.AsNoTracking().ToList();
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void UpdatePrice(int productId, decimal newPrice)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            product.Price = newPrice;
        }
    }

    public void Delete(int productId)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}