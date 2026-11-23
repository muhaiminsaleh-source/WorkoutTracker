using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace Milestone02_OOP
{
    internal class Program
    {
        static string filePath = "workouts_m2.json";
        static List<Workout> workouts = new List<Workout>();

        static void Main(string[] args)
        {
            LoadWorkouts();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Workout Tracker (Milestone 02 - OOP) ---");
                Console.WriteLine("1. Add Workout");
                Console.WriteLine("2. List Workouts");
                Console.WriteLine("3. Update Duration");
                Console.WriteLine("4. Delete Workout");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddWorkout();
                else if (choice == "2") ListWorkouts();
                else if (choice == "3") UpdateWorkoutDuration();
                else if (choice == "4") DeleteWorkout();
                else if (choice == "0") running = false;
                else Console.WriteLine("Invalid option.");
            }
        }

        static void AddWorkout()
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

                int nextId = workouts.Count == 0 ? 1 : workouts[workouts.Count - 1].Id + 1;
                Workout workout = new Workout(nextId, name, minutes, group, DateTime.Now);

                workouts.Add(workout);
                SaveWorkouts();
                Console.WriteLine("Workout added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ListWorkouts()
        {
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

        static void UpdateWorkoutDuration()
        {
            ListWorkouts();
            if (workouts.Count == 0) return;

            Console.Write("Enter Workout ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Workout? found = workouts.Find(w => w.Id == id);
            if (found == null)
            {
                Console.WriteLine("Workout not found.");
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
                found.UpdateDuration(newMinutes);
                SaveWorkouts();
                Console.WriteLine("Duration updated.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteWorkout()
        {
            ListWorkouts();
            if (workouts.Count == 0) return;

            Console.Write("Enter Workout ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Workout? found = workouts.Find(w => w.Id == id);
            if (found != null)
            {
                workouts.Remove(found);
                SaveWorkouts();
                Console.WriteLine("Workout deleted.");
            }
            else
            {
                Console.WriteLine("Workout not found.");
            }
        }

        static void SaveWorkouts()
        {
            string json = JsonSerializer.Serialize(workouts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        static void LoadWorkouts()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                workouts = JsonSerializer.Deserialize<List<Workout>>(json) ?? new List<Workout>();
            }
        }
    }
}