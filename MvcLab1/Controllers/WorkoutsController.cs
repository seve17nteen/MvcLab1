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

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var workout = _repository.GetById(id);

            if (workout == null)
                return NotFound();

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
                _repository.Add(workout);
                return RedirectToAction(nameof(Index));
            }

            return View(workout);
        }

        public IActionResult Edit(int id)
        {
            var workout = _repository.GetById(id);

            if (workout == null)
                return NotFound();

            return View(workout);
        }

        [HttpPost]
        public IActionResult Edit(Workout workout)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(workout);
                return RedirectToAction(nameof(Index));
            }

            return View(workout);
        }

        public IActionResult Delete(int id)
        {
            var workout = _repository.GetById(id);

            if (workout == null)
                return NotFound();

            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
