using Microsoft.EntityFrameworkCore;
using webapi.DAL.models;

namespace webapi.DAL
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseLog> ExerciseLogs { get; set; }
    public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Exercise>().ToTable("Exercises");
      modelBuilder.Entity<ExerciseLog>().ToTable("ExerciseLogs");
      modelBuilder.Entity<WorkoutProgram>().ToTable("WorkoutPrograms");
      modelBuilder.Entity<User>().ToTable("ApiUsers");

      modelBuilder.Entity<Exercise>()
          .HasKey(ex => ex._id);

      modelBuilder.Entity<ExerciseLog>()
          .HasKey(ex => ex._id);

      modelBuilder.Entity<WorkoutProgram>()
          .HasKey(ex => ex._id);

      modelBuilder.Entity<WorkoutProgram>()
          .HasMany(e => e.ExerciseList)
          .WithOne(e => e.WorkoutProgram)
          .HasForeignKey(e => e.WorkoutProgramId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<WorkoutProgram>()
          .HasMany(e => e.Logs)
          .WithOne(e => e.WorkoutProgram)
          .HasForeignKey(e => e.WorkoutProgramId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<User>()
          .HasKey(ex => ex._id);
    }
  }
}
