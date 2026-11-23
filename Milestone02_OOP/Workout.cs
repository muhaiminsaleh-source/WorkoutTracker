using System;

namespace Milestone02_OOP
{
    public class Workout
    {
        public int Id { get; private set; }
        public string ExerciseName { get; private set; }
        public int DurationMinutes { get; private set; }
        public string MuscleGroup { get; private set; }
        public DateTime Date { get; private set; }

        public Workout(int id, string exerciseName, int durationMinutes, string muscleGroup, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(exerciseName))
                throw new ArgumentException("Exercise name cannot be empty.");

            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than 0.");

            Id = id;
            ExerciseName = exerciseName.Trim();
            DurationMinutes = durationMinutes;
            MuscleGroup = string.IsNullOrWhiteSpace(muscleGroup) ? "General" : muscleGroup.Trim();
            Date = date;
        }

        public void UpdateDuration(int newMinutes)
        {
            if (newMinutes <= 0)
                throw new ArgumentException("Duration must be greater than 0.");

            DurationMinutes = newMinutes;
        }

        public string GetSummary()
        {
            return $"[{Id}] {ExerciseName} | {DurationMinutes} min | {MuscleGroup} | {Date:yyyy-MM-dd}";
        }
    }
}