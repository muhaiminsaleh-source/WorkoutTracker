using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Milestone01_BasicConsole
{
    internal class Program
    {
        static string filePath = "workouts.json";
        static List<string> workouts = new List<string>();

        static void Main(string[] args)
        {
            LoadWorkouts();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Workout Tracker (Milestone 01) ---");
                Console.WriteLine("1. Add Workout");
                Console.WriteLine("2. List Workouts");
                Console.WriteLine("3. Find Workout");
                Console.WriteLine("4. Delete Workout");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddWorkout();
                else if (choice == "2") ListWorkouts();
                else if (choice == "3") FindWorkout();
                else if (choice == "4") DeleteWorkout();
                else if (choice == "0") running = false;
                else Console.WriteLine("Invalid option.");
            }
        }

        static void AddWorkout()
        {
            Console.Write("Exercise Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Duration (minutes): ");
            string durationText = Console.ReadLine() ?? "";
            if (!int.TryParse(durationText, out int minutes) || minutes <= 0)
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            Console.Write("Muscle Group: ");
            string group = Console.ReadLine() ?? "";

            string record = $"{name} | {minutes} min | {group}";
            workouts.Add(record);
            SaveWorkouts();
            Console.WriteLine("Workout added.");
        }

        static void ListWorkouts()
        {
            if (workouts.Count == 0)
            {
                Console.WriteLine("No workouts found.");
                return;
            }

            for (int i = 0; i < workouts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {workouts[i]}");
            }
        }

        static void FindWorkout()
        {
            Console.Write("Search keyword: ");
            string keyword = (Console.ReadLine() ?? "").ToLower();
            bool found = false;

            for (int i = 0; i < workouts.Count; i++)
            {
                if (workouts[i].ToLower().Contains(keyword))
                {
                    Console.WriteLine($"{i + 1}. {workouts[i]}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No matching workout found.");
            }
        }

        static void DeleteWorkout()
        {
            ListWorkouts();
            if (workouts.Count == 0) return;

            Console.Write("Enter number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= workouts.Count)
            {
                workouts.RemoveAt(index - 1);
                SaveWorkouts();
                Console.WriteLine("Workout deleted.");
            }
            else
            {
                Console.WriteLine("Invalid number.");
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
                workouts = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
        }
    }
}