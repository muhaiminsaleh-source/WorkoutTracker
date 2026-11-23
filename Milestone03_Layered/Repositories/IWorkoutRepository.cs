using System.Collections.Generic;
using Milestone03_Layered.Models;

namespace Milestone03_Layered.Repositories
{
    public interface IWorkoutRepository
    {
        List<Workout> GetAll();
        Workout? GetById(int id);
        void Add(Workout workout);
        void Update(Workout workout);
        bool Delete(int id);
    }
}