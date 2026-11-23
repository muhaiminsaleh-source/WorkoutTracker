using System;
using System.Collections.Generic;
using System.Linq;
using Milestone04_Database.Data;
using Milestone04_Database.Models;

namespace Milestone04_Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new WorkoutDbContext())
            {
                db.Database.EnsureCreated();
            }

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Workout Tracker (Milestone 04 - SQLite Database) ---");
                Console.WriteLine("1. Add Workout");
                Console.WriteLine("2. List Workouts");
                Console.WriteLine("3. Search Workouts");
                Console.WriteLine("4. Update Duration");
                Console.WriteLine("5. Delete Workout");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddWorkout();
                else if (choice == "2") ListWorkouts();
                else if (choice == "3") SearchWorkouts();
                else if (choice == "4") UpdateDuration();
                else if (choice == "5") DeleteWorkout();
                else if (choice == "0") running = false;
                else Console.WriteLine("Invalid option.");
            }
        }

        static void AddWorkout()
        {
            Console.Write("Exercise Name: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Exercise name cannot be empty.");
                return;
            }

            Console.Write("Duration (minutes): ");
            if (!int.TryParse(Console.ReadLine(), out int minutes) || minutes <= 0)
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            Console.Write("Muscle Group: ");
            string group = (Console.ReadLine() ?? "").Trim();

            using var db = new WorkoutDbContext();
            var workout = new Workout
            {
                ExerciseName = name,
                DurationMinutes = minutes,
                MuscleGroup = string.IsNullOrWhiteSpace(group) ? "General" : group,
                Date = DateTime.Now
            };

            db.Workouts.Add(workout);
            db.SaveChanges();
            Console.WriteLine("Workout saved to SQLite database.");
        }

        static void ListWorkouts()
        {
            using var db = new WorkoutDbContext();
            List<Workout> workouts = db.Workouts.OrderBy(w => w.Id).ToList();

            if (workouts.Count == 0)
            {
                Console.WriteLine("No workouts found.");
                return;
            }

            foreach (var w in workouts)
            {
                Console.WriteLine(w.GetSummary());
            }
        }

        static void SearchWorkouts()
        {
            Console.Write("Search keyword: ");
            string keyword = (Console.ReadLine() ?? "").Trim().ToLower();

            using var db = new WorkoutDbContext();
            var results = db.Workouts
                .Where(w => w.ExerciseName.ToLower().Contains(keyword) || w.MuscleGroup.ToLower().Contains(keyword))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No matching workouts found.");
                return;
            }

            foreach (var w in results)
            {
                Console.WriteLine(w.GetSummary());
            }
        }

        static void UpdateDuration()
        {
            ListWorkouts();
            Console.Write("Enter Workout ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            using var db = new WorkoutDbContext();
            var workout = db.Workouts.Find(id);
            if (workout == null)
            {
                Console.WriteLine("Workout not found.");
                return;
            }

            Console.Write("New Duration (minutes): ");
            if (!int.TryParse(Console.ReadLine(), out int newMinutes) || newMinutes <= 0)
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            workout.DurationMinutes = newMinutes;
            db.SaveChanges();
            Console.WriteLine("Duration updated in database.");
        }

        static void DeleteWorkout()
        {
            ListWorkouts();
            Console.Write("Enter Workout ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            using var db = new WorkoutDbContext();
            var workout = db.Workouts.Find(id);
            if (workout == null)
            {
                Console.WriteLine("Workout not found.");
                return;
            }

            db.Workouts.Remove(workout);
            db.SaveChanges();
            Console.WriteLine("Workout deleted from database.");
        }
    }
}