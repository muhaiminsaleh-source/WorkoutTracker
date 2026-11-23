using Microsoft.EntityFrameworkCore;
using Milestone05_WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<WorkoutDbContext>(options =>
    options.UseSqlite("Data Source=workouts_api.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();