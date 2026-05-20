using Microsoft.EntityFrameworkCore;
using MvcLab1.Data;
using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public class EfWorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _context;

        public EfWorkoutRepository(AppDbContext context)
        {
            _context = context;
        }

        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========

        public IEnumerable<Workout> GetAll() => _context.Workouts.ToList();

        public Workout? GetById(int id) => _context.Workouts.Find(id);

        public void Add(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }

        public void Update(Workout workout)
        {
            _context.Workouts.Update(workout);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var workout = _context.Workouts.Find(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }
        }

        // ========== НОВЫЕ LINQ-МЕТОДЫ ==========

        /// <summary>Фильтрация по длительности</summary>
        public IEnumerable<Workout> GetByDurationRange(int minDuration, int maxDuration)
        {
            return _context.Workouts
                .Where(w => w.Duration >= minDuration && w.Duration <= maxDuration)
                .OrderBy(w => w.Duration)
                .ToList();
        }

        /// <summary>Топ N самых калорийных тренировок</summary>
        public IEnumerable<Workout> GetTopCalorieBurning(int count)
        {
            return _context.Workouts
                .OrderByDescending(w => w.Calories)
                .Take(count)
                .ToList();
        }

        /// <summary>Поиск по названию, типу или инструктору</summary>
        public IEnumerable<Workout> SearchWorkouts(string searchTerm)
        {
            return _context.Workouts
                .Where(w => w.Name.Contains(searchTerm) ||
                            w.Type.Contains(searchTerm) ||
                            w.Instructor.Contains(searchTerm))
                .OrderBy(w => w.Name)
                .ToList();
        }

        /// <summary>Среднее количество калорий</summary>
        public double GetAverageCalories()
        {
            return _context.Workouts.Average(w => w.Calories);
        }

        /// <summary>Общее количество тренировок</summary>
        public int GetTotalCount()
        {
            return _context.Workouts.Count();
        }

        /// <summary>Диапазон длительности</summary>
        public (int MinDuration, int MaxDuration) GetDurationRange()
        {
            return (
                MinDuration: _context.Workouts.Min(w => w.Duration),
                MaxDuration: _context.Workouts.Max(w => w.Duration)
            );
        }

        /// <summary>Проверка наличия тренировок указанного типа</summary>
        public bool AnyWorkoutOfType(string type)
        {
            return _context.Workouts.Any(w => w.Type == type);
        }

        /// <summary>Группировка по типу тренировки (ИСПРАВЛЕНО)</summary>
        public IEnumerable<IGrouping<string, Workout>> GetWorkoutsGroupedByType()
        {
            // Сначала получаем данные из БД, затем группируем в памяти
            var workouts = _context.Workouts.ToList();
            return workouts
                .GroupBy(w => w.Type)
                .OrderBy(g => g.Key)
                .ToList();
        }

        /// <summary>Пагинация</summary>
        public IEnumerable<Workout> GetWorkoutsWithPagination(int page, int pageSize)
        {
            return _context.Workouts
                .OrderBy(w => w.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>Общее количество страниц</summary>
        public int GetTotalPages(int pageSize)
        {
            int totalCount = _context.Workouts.Count();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        // ========== АСИНХРОННЫЕ МЕТОДЫ ==========

        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            return await _context.Workouts.ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(int id)
        {
            return await _context.Workouts.FindAsync(id);
        }

        public async Task<IEnumerable<Workout>> SearchWorkoutsAsync(string searchTerm)
        {
            return await _context.Workouts
                .Where(w => w.Name.Contains(searchTerm) ||
                            w.Type.Contains(searchTerm) ||
                            w.Instructor.Contains(searchTerm))
                .OrderBy(w => w.Name)
                .ToListAsync();
        }

        public async Task<double> GetAverageCaloriesAsync()
        {
            return await _context.Workouts.AverageAsync(w => w.Calories);
        }

        /// <summary>Асинхронная группировка по типу тренировки</summary>
        public async Task<IEnumerable<IGrouping<string, Workout>>> GetWorkoutsGroupedByTypeAsync()
        {
            var workouts = await _context.Workouts.ToListAsync();
            return workouts
                .GroupBy(w => w.Type)
                .OrderBy(g => g.Key)
                .ToList();
        }
    }
}