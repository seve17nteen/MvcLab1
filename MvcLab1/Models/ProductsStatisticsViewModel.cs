namespace MvcLab1.Models
{
    public class ProductsStatisticsViewModel
    {
        public int TotalCount { get; set; }
        public decimal AveragePrice { get; set; }
        public int InStockCount { get; set; }
        public (decimal MinPrice, decimal MaxPrice) PriceRange { get; set; }
        public IEnumerable<CategoryStatViewModel> Categories { get; set; }
    }

    public class CategoryStatViewModel
    {
        public string Category { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}