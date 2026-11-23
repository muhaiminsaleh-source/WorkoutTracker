using Microsoft.EntityFrameworkCore;
using Milestone04_Database.Models;

namespace Milestone04_Database.Data
{
    public class WorkoutDbContext : DbContext
    {
        public DbSet<Workout> Workouts => Set<Workout>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=workouts.db");
        }
    }
}