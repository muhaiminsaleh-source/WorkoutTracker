using Microsoft.EntityFrameworkCore;
using Milestone05_WebApi.Models;

namespace Milestone05_WebApi.Data
{
    public class WorkoutDbContext : DbContext
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts => Set<Workout>();
    }
}