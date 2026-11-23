using System;
using System.Collections.Generic;
using Milestone03_Layered.Models;
using Milestone03_Layered.Repositories;
using Milestone03_Layered.Services;

namespace Milestone03_Layered
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWorkoutRepository repository = new JsonWorkoutRepository();
            WorkoutService service = new WorkoutService(repository);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Workout Tracker (Milestone 03 - Layered) ---");
                Console.WriteLine("1. Add Workout");
                Console.WriteLine("2. List Workouts");
                Console.WriteLine("3. Search Workouts");
                Console.WriteLine("4. Update Duration");
                Console.WriteLine("5. Delete Workout");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddWorkoutUI(service);
                else if (choice == "2") ListWorkoutsUI(service);
                else if (choice == "3") SearchWorkoutsUI(service);
                else if (choice == "4") UpdateDurationUI(service);
                else if (choice == "5") DeleteWorkoutUI(service);
                else if (choice == "0") running = false;
                else Console.WriteLine("Invalid option.");
            }
        }

        static void AddWorkoutUI(WorkoutService service)
        {
            try
            {
                Console.Write("Exercise Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Duration (minutes): ");
                if (!int.TryParse(Console.ReadLine(), out int minutes))
                {
                    Console.WriteLine("Invalid number.");
                    return;
                }

                Console.Write("Muscle Group: ");
                string group = Console.ReadLine() ?? "";

                service.AddWorkout(name, minutes, group);
                Console.WriteLine("Workout added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ListWorkoutsUI(WorkoutService service)
        {
            List<Workout> workouts = service.GetAllWorkouts();
            if (workouts.Count == 0)
            {
                Console.WriteLine("No workouts found.");
                return;
            }

            foreach (Workout w in workouts)
            {
                Console.WriteLine(w.GetSummary());
            }
        }

        static void SearchWorkoutsUI(WorkoutService service)
        {
            Console.Write("Search keyword: ");
            string keyword = Console.ReadLine() ?? "";
            List<Workout> results = service.SearchWorkouts(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("No matching workouts found.");
                return;
            }

            foreach (Workout w in results)
            {
                Console.WriteLine(w.GetSummary());
            }
        }

        static void UpdateDurationUI(WorkoutService service)
        {
            ListWorkoutsUI(service);
            Console.Write("Enter Workout ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Console.Write("New Duration (minutes): ");
            if (!int.TryParse(Console.ReadLine(), out int newMinutes))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            try
            {
                bool updated = service.UpdateDuration(id, newMinutes);
                Console.WriteLine(updated ? "Duration updated." : "Workout not found.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteWorkoutUI(WorkoutService service)
        {
            ListWorkoutsUI(service);
            Console.Write("Enter Workout ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            bool deleted = service.DeleteWorkout(id);
            Console.WriteLine(deleted ? "Workout deleted." : "Workout not found.");
        }
    }
}