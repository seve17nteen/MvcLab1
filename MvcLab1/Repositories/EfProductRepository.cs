using Microsoft.EntityFrameworkCore;
using MvcLab1.Data;
using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public EfProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========

        public IEnumerable<Product> GetAll() => _context.Products.ToList();
        public Product? GetById(int id) => _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetByCategory(string category) =>
            _context.Products.Where(p => p.Category == category).ToList();

        public IEnumerable<Product> GetInStock() =>
            _context.Products.Where(p => p.InStock).ToList();

        // ========== НОВЫЕ LINQ-МЕТОДЫ ==========

        public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToList();
        }

        public IEnumerable<Product> GetTopExpensiveProducts(int count)
        {
            return _context.Products
                .OrderByDescending(p => p.Price)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _context.Products
                .Where(p => p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm) ||
                            p.Category.Contains(searchTerm))
                .OrderBy(p => p.Name)
                .ToList();
        }

        public decimal GetAveragePrice()
        {
            return _context.Products.Average(p => p.Price);
        }

        public int GetTotalCount()
        {
            return _context.Products.Count();
        }

        public (decimal MinPrice, decimal MaxPrice) GetPriceRange()
        {
            return (
                MinPrice: _context.Products.Min(p => p.Price),
                MaxPrice: _context.Products.Max(p => p.Price)
            );
        }

        public bool AnyInCategory(string category)
        {
            return _context.Products.Any(p => p.Category == category);
        }

        /// <summary>Группировка по категориям (ИСПРАВЛЕНО)</summary>
        public IEnumerable<IGrouping<string, Product>> GetProductsGroupedByCategory()
        {
            // Сначала получаем данные из БД, затем группируем в памяти
            var products = _context.Products.ToList();
            return products
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .ToList();
        }

        public IEnumerable<Product> GetProductsWithPagination(int page, int pageSize)
        {
            return _context.Products
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            int totalCount = _context.Products.Count();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        // ========== АСИНХРОННЫЕ МЕТОДЫ ==========

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToListAsync();
        }

        public async Task<decimal> GetAveragePriceAsync()
        {
            return await _context.Products.AverageAsync(p => p.Price);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        /// <summary>Асинхронная группировка по категориям (ИСПРАВЛЕНО)</summary>
        public async Task<IEnumerable<IGrouping<string, Product>>> GetProductsGroupedByCategoryAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .ToList();
        }
    }
}