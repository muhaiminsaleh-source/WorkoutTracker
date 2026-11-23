using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milestone05_WebApi.Data;
using Milestone05_WebApi.Models;

namespace Milestone05_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly WorkoutDbContext _db;

        public WorkoutsController(WorkoutDbContext db)
        {
            _db = db;
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<List<Workout>>> GetAll()
        {
            return await _db.Workouts.OrderBy(w => w.Id).ToListAsync();
        }

        // GET: api/workouts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetById(int id)
        {
            var workout = await _db.Workouts.FindAsync(id);
            if (workout == null) return NotFound();
            return workout;
        }

        // POST: api/workouts
        [HttpPost]
        public async Task<ActionResult<Workout>> Create(Workout workout)
        {
            if (string.IsNullOrWhiteSpace(workout.ExerciseName) || workout.DurationMinutes <= 0)
                return BadRequest("Valid exercise name and duration (> 0) are required.");

            workout.Date = DateTime.Now;
            _db.Workouts.Add(workout);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = workout.Id }, workout);
        }

        // PUT: api/workouts/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Workout updated)
        {
            var existing = await _db.Workouts.FindAsync(id);
            if (existing == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updated.ExerciseName) || updated.DurationMinutes <= 0)
                return BadRequest("Valid exercise name and duration (> 0) are required.");

            existing.ExerciseName = updated.ExerciseName.Trim();
            existing.DurationMinutes = updated.DurationMinutes;
            existing.MuscleGroup = string.IsNullOrWhiteSpace(updated.MuscleGroup) ? "General" : updated.MuscleGroup.Trim();

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/workouts/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workout = await _db.Workouts.FindAsync(id);
            if (workout == null) return NotFound();

            _db.Workouts.Remove(workout);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}