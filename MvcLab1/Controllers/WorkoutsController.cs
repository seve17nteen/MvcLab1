using Microsoft.AspNetCore.Mvc;
using MvcLab1.Models;
using MvcLab1.Repositories;

namespace MvcLab1.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutRepository _repository;

        public WorkoutsController(IWorkoutRepository repository)
        {
            _repository = repository;
        }

        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Workout workout)
        {
            if (ModelState.IsValid)
            {
                workout.Date = DateTime.Now;
                _repository.Add(workout);
                TempData["SuccessMessage"] = "Тренировка добавлена!";
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        public IActionResult Edit(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        [HttpPost]
        public IActionResult Edit(Workout workout)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(workout);
                TempData["SuccessMessage"] = "Тренировка обновлена!";
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        public IActionResult Delete(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Тренировка удалена!";
            return RedirectToAction(nameof(Index));
        }

        // ========== НОВЫЕ ДЕЙСТВИЯ ДЛЯ LINQ-ЗАПРОСОВ ==========

        /// <summary>Фильтрация по длительности: /Workouts/ByDuration?min=15&max=45</summary>
        public IActionResult ByDuration(int min, int max)
        {
            var workouts = _repository.GetByDurationRange(min, max);
            ViewBag.MinDuration = min;
            ViewBag.MaxDuration = max;
            ViewBag.Title = $"Тренировки от {min} до {max} минут";
            return View(workouts);
        }

        /// <summary>Топ самых калорийных: /Workouts/TopCalorieBurning?count=5</summary>
        public IActionResult TopCalorieBurning(int count = 5)
        {
            var workouts = _repository.GetTopCalorieBurning(count);
            ViewBag.Title = $"Топ {count} самых калорийных тренировок";
            ViewBag.Count = count;
            return View(workouts);
        }

        /// <summary>Поиск: /Workouts/Search?term=йога</summary>
        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return RedirectToAction(nameof(Index));
            }
            var workouts = _repository.SearchWorkouts(term);
            ViewBag.SearchTerm = term;
            ViewBag.Title = $"Результаты поиска: {term}";
            ViewBag.Count = workouts.Count();
            return View(workouts);
        }

        /// <summary>Статистика: /Workouts/Statistics</summary>
        public IActionResult Statistics()
        {
            var stats = new WorkoutsStatisticsViewModel
            {
                TotalCount = _repository.GetTotalCount(),
                AverageCalories = _repository.GetAverageCalories(),
                DurationRange = _repository.GetDurationRange(),
                HasHighIntensity = _repository.AnyWorkoutOfType("HIIT"),
                Types = _repository.GetWorkoutsGroupedByType()
                    .Select(g => new TypeStatViewModel
                    {
                        Type = g.Key,
                        Count = g.Count(),
                        AverageCalories = g.Average(w => w.Calories),
                        AverageDuration = g.Average(w => w.Duration)
                    }).OrderBy(t => t.Type)
            };
            return View(stats);
        }

        /// <summary>Группировка по типам: /Workouts/GroupedByType</summary>
        public IActionResult GroupedByType()
        {
            var workouts = _repository.GetAll();
            return View(workouts);
        }

        /// <summary>Пагинация: /Workouts/Paginated?page=1</summary>
        public IActionResult Paginated(int page = 1, int pageSize = 4)
        {
            var workouts = _repository.GetWorkoutsWithPagination(page, pageSize);
            var totalPages = _repository.GetTotalPages(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;

            return View(workouts);
        }
    }
}