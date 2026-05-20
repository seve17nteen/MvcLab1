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

        public IEnumerable<Workout> GetAll()
        {
            return _context.Workouts.ToList();
        }

        public Workout? GetById(int id)
        {
            return _context.Workouts.Find(id);
        }

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
    }
}