using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public class InMemoryWorkoutRepository : IWorkoutRepository
    {
        private readonly List<Workout> _workouts;
        private int _nextId = 1;

        public InMemoryWorkoutRepository()
        {
            _workouts = new List<Workout>();
            SeedData();
        }

        private void SeedData()
        {
            Add(new Workout
            {
                Name = "Силовая тренировка",
                Type = "Силовая",
                Duration = 60,
                Calories = 500,
                Difficulty = "Средняя",
                Instructor = "Иван Петров",
                Date = DateTime.Now
            });

            Add(new Workout
            {
                Name = "Кардио тренировка",
                Type = "Кардио",
                Duration = 45,
                Calories = 400,
                Difficulty = "Высокая",
                Instructor = "Анна Смирнова",
                Date = DateTime.Now
            });

            Add(new Workout
            {
                Name = "Йога",
                Type = "Йога",
                Duration = 50,
                Calories = 200,
                Difficulty = "Низкая",
                Instructor = "Мария Иванова",
                Date = DateTime.Now
            });
        }

        public IEnumerable<Workout> GetAll() => _workouts;

        public Workout? GetById(int id) =>
            _workouts.FirstOrDefault(w => w.Id == id);

        public void Add(Workout workout)
        {
            workout.Id = _nextId++;
            _workouts.Add(workout);
        }

        public void Update(Workout workout)
        {
            var existing = GetById(workout.Id);

            if (existing != null)
            {
                existing.Name = workout.Name;
                existing.Type = workout.Type;
                existing.Duration = workout.Duration;
                existing.Calories = workout.Calories;
                existing.Difficulty = workout.Difficulty;
                existing.Instructor = workout.Instructor;
                existing.Date = workout.Date;
            }
        }

        public void Delete(int id)
        {
            var workout = GetById(id);

            if (workout != null)
                _workouts.Remove(workout);
        }
    }
}
