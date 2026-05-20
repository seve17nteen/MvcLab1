using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public interface IProductRepository
    {
        // Существующие методы
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        IEnumerable<Product> GetByCategory(string category);
        IEnumerable<Product> GetInStock();

        // ========== НОВЫЕ LINQ-МЕТОДЫ ==========
        IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        IEnumerable<Product> GetTopExpensiveProducts(int count);
        IEnumerable<Product> SearchProducts(string searchTerm);
        decimal GetAveragePrice();
        int GetTotalCount();
        (decimal MinPrice, decimal MaxPrice) GetPriceRange();
        bool AnyInCategory(string category);
        IEnumerable<IGrouping<string, Product>> GetProductsGroupedByCategory();
        IEnumerable<Product> GetProductsWithPagination(int page, int pageSize);
        int GetTotalPages(int pageSize);

        // ========== АСИНХРОННЫЕ ВЕРСИИ ==========
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<decimal> GetAveragePriceAsync();
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<IGrouping<string, Product>>> GetProductsGroupedByCategoryAsync();
    }
}