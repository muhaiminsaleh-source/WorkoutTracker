using System;

namespace Milestone04_Database.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string MuscleGroup { get; set; } = "General";
        public DateTime Date { get; set; } = DateTime.Now;

        public string GetSummary()
        {
            return $"[{Id}] {ExerciseName} | {DurationMinutes} min | {MuscleGroup} | {Date:yyyy-MM-dd}";
        }
    }
}