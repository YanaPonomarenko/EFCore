using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Services;

public interface IProductService
{
    void CreateProduct(string name, decimal price, int stockQuantity);
    List<Product> GetAllProducts();
    void UpdateProductPrice(int productId, decimal newPrice);
    void DeleteProduct(int productId);
    Product GetProductById(int id);
}
