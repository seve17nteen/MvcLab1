namespace MvcLab1.Models
{
    public class WorkoutsStatisticsViewModel
    {
        public int TotalCount { get; set; }
        public double AverageCalories { get; set; }
        public (int MinDuration, int MaxDuration) DurationRange { get; set; }
        public bool HasHighIntensity { get; set; }
        public IEnumerable<TypeStatViewModel> Types { get; set; }
    }

    public class TypeStatViewModel
    {
        public string Type { get; set; } = string.Empty;
        public int Count { get; set; }
        public double AverageCalories { get; set; }
        public double AverageDuration { get; set; }
    }
}