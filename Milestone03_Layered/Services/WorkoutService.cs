using System;
using System.Collections.Generic;
using System.Linq;
using Milestone03_Layered.Models;
using Milestone03_Layered.Repositories;

namespace Milestone03_Layered.Services
{
    public class WorkoutService
    {
        private readonly IWorkoutRepository _repository;

        public WorkoutService(IWorkoutRepository repository)
        {
            _repository = repository;
        }

        public void AddWorkout(string name, int durationMinutes, string muscleGroup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Exercise name cannot be empty.");

            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than 0.");

            Workout workout = new Workout
            {
                ExerciseName = name.Trim(),
                DurationMinutes = durationMinutes,
                MuscleGroup = string.IsNullOrWhiteSpace(muscleGroup) ? "General" : muscleGroup.Trim(),
                Date = DateTime.Now
            };

            _repository.Add(workout);
        }

        public List<Workout> GetAllWorkouts()
        {
            return _repository.GetAll();
        }

        public List<Workout> SearchWorkouts(string keyword)
        {
            string lower = (keyword ?? "").Trim().ToLower();
            return _repository.GetAll()
                .Where(w => w.ExerciseName.ToLower().Contains(lower) || w.MuscleGroup.ToLower().Contains(lower))
                .ToList();
        }

        public bool UpdateDuration(int id, int newMinutes)
        {
            if (newMinutes <= 0)
                throw new ArgumentException("Duration must be greater than 0.");

            Workout? workout = _repository.GetById(id);
            if (workout == null) return false;

            workout.DurationMinutes = newMinutes;
            _repository.Update(workout);
            return true;
        }

        public bool DeleteWorkout(int id)
        {
            return _repository.Delete(id);
        }
    }
}