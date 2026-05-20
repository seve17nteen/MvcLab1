using System.Collections.Generic;
using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);  // ← ДОБАВЬТЕ ЗНАК ВОПРОСА (nullable)
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        IEnumerable<Product> GetByCategory(string category);
        IEnumerable<Product> GetInStock();
    }
}