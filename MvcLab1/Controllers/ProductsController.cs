using Microsoft.AspNetCore.Mvc;
using MvcLab1.Models;
using MvcLab1.Repositories;

namespace MvcLab1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;

        // Внедрение зависимости через конструктор
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: /Products
        public IActionResult Index()
        {
            var products = _repository.GetAll();
            return View(products);
        }

        // GET: /Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // GET: /Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                _repository.Add(product);
                TempData["SuccessMessage"] = "Товар успешно добавлен!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: /Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(product);
                    TempData["SuccessMessage"] = "Товар успешно обновлен!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Товар удален!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Products/Category/Электроника
        public IActionResult Category(string category)
        {
            var products = _repository.GetByCategory(category);
            ViewBag.Category = category;
            return View(products);
        }

        // GET: /Products/InStock
        public IActionResult InStock()
        {
            var products = _repository.GetInStock();
            return View("Index", products);
        }

        // ========== НОВЫЕ ДЕЙСТВИЯ ДЛЯ LINQ-ЗАПРОСОВ ==========

        // GET: /Products/ByPrice?min=100&max=1000
        public IActionResult ByPrice(decimal min, decimal max)
        {
            var products = _repository.GetProductsByPriceRange(min, max);
            ViewBag.MinPrice = min;
            ViewBag.MaxPrice = max;
            ViewBag.Title = $"Товары от {min:C} до {max:C}";
            return View(products);
        }

        // GET: /Products/TopExpensive?count=5
        public IActionResult TopExpensive(int count = 5)
        {
            var products = _repository.GetTopExpensiveProducts(count);
            ViewBag.Title = $"Топ {count} самых дорогих товаров";
            ViewBag.Count = count;
            return View(products);
        }

        // GET: /Products/Search?term=ноутбук
        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return RedirectToAction(nameof(Index));
            }
            var products = _repository.SearchProducts(term);
            ViewBag.SearchTerm = term;
            ViewBag.Title = $"Результаты поиска: {term}";
            ViewBag.Count = products.Count();
            return View(products);
        }

        // GET: /Products/Statistics
        public IActionResult Statistics()
        {
            var stats = new ProductsStatisticsViewModel
            {
                TotalCount = _repository.GetTotalCount(),
                AveragePrice = _repository.GetAveragePrice(),
                InStockCount = _repository.GetInStock().Count(),
                PriceRange = _repository.GetPriceRange(),
                Categories = _repository.GetProductsGroupedByCategory()
                    .Select(g => new CategoryStatViewModel
                    {
                        Category = g.Key ?? "Без категории",
                        Count = g.Count(),
                        AveragePrice = g.Average(p => p.Price),
                        MinPrice = g.Min(p => p.Price),
                        MaxPrice = g.Max(p => p.Price)
                    }).OrderBy(c => c.Category)
            };
            return View(stats);
        }

        // GET: /Products/GroupedByCategory
        public IActionResult GroupedByCategory()
        {
            var products = _repository.GetAll();
            return View(products);
        }

        // GET: /Products/Paginated?page=1
        public IActionResult Paginated(int page = 1, int pageSize = 5)
        {
            var products = _repository.GetProductsWithPagination(page, pageSize);
            var totalPages = _repository.GetTotalPages(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;

            return View(products);
        }
    }
}