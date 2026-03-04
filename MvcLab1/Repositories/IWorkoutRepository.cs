using MvcLab1.Models;

namespace MvcLab1.Repositories
{
    public interface IWorkoutRepository
    {
        IEnumerable<Workout> GetAll();

        Workout? GetById(int id);

        void Add(Workout workout);

        void Update(Workout workout);

        void Delete(int id);
    }
}
