using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Milestone03_Layered.Models;

namespace Milestone03_Layered.Repositories
{
    public class JsonWorkoutRepository : IWorkoutRepository
    {
        private readonly string _filePath;
        private List<Workout> _workouts;

        public JsonWorkoutRepository(string filePath = "workouts_m3.json")
        {
            _filePath = filePath;
            _workouts = Load();
        }

        public List<Workout> GetAll() => _workouts;

        public Workout? GetById(int id) => _workouts.FirstOrDefault(w => w.Id == id);

        public void Add(Workout workout)
        {
            workout.Id = _workouts.Count == 0 ? 1 : _workouts.Max(w => w.Id) + 1;
            _workouts.Add(workout);
            Save();
        }

        public void Update(Workout workout)
        {
            Save();
        }

        public bool Delete(int id)
        {
            Workout? item = GetById(id);
            if (item == null) return false;

            _workouts.Remove(item);
            Save();
            return true;
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(_workouts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        private List<Workout> Load()
        {
            if (!File.Exists(_filePath)) return new List<Workout>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Workout>>(json) ?? new List<Workout>();
        }
    }
}