using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public interface IWorkoutRepository
    {
        // Существующие методы
        IEnumerable<Workout> GetAll();
        Workout? GetById(int id);
        void Add(Workout workout);
        void Update(Workout workout);
        void Delete(int id);

        // ========== НОВЫЕ LINQ-МЕТОДЫ ДЛЯ WORKOUT ==========

        // Фильтрация по длительности
        IEnumerable<Workout> GetByDurationRange(int minDuration, int maxDuration);

        // Топ N самых калорийных тренировок
        IEnumerable<Workout> GetTopCalorieBurning(int count);

        // Поиск по названию, типу, инструктору
        IEnumerable<Workout> SearchWorkouts(string searchTerm);

        // Статистика
        double GetAverageCalories();
        int GetTotalCount();
        (int MinDuration, int MaxDuration) GetDurationRange();
        bool AnyWorkoutOfType(string type);

        // Группировка по типу тренировки
        IEnumerable<IGrouping<string, Workout>> GetWorkoutsGroupedByType();

        // Пагинация
        IEnumerable<Workout> GetWorkoutsWithPagination(int page, int pageSize);
        int GetTotalPages(int pageSize);

        // Асинхронные версии
        Task<IEnumerable<Workout>> GetAllAsync();
        Task<Workout?> GetByIdAsync(int id);
        Task<IEnumerable<Workout>> SearchWorkoutsAsync(string searchTerm);
        Task<double> GetAverageCaloriesAsync();
    }
}