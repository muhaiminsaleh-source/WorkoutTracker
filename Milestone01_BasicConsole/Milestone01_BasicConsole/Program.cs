using System;
using System.Collections.Generic;

namespace Milestone01_BasicConsole
{
    internal class Program
    {
        static List<string> workouts = new List<string>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Workout Tracker (Milestone 01) ---");
                Console.WriteLine("1. Add Workout");
                Console.WriteLine("2. List Workouts");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddWorkout();
                else if (choice == "2") ListWorkouts();
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
    }
}