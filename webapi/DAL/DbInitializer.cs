using System;
using System.Collections.Generic;
using System.Linq;
using webapi.DAL;
using webapi.DAL.models;
using webapi.DAL.repos;

namespace webapi.DAL
{
  public static class DbInitializer
  {

    public static void Initialize(ApplicationContext context, WorkoutProgramRepo repo)
    {
      context.Database.EnsureCreated();

      // Look for any students.
      if (context.WorkoutPrograms.Any())
      {
        return;   // DB has been seeded
      }

      var WorkoutPrograms = new WorkoutProgram[]
      {
              new WorkoutProgram
              {
                Name = "test",
                ExerciseList = new List<Exercise>
                {
                  new Exercise
                  {
                    Description = "asd",
                    ExerciseName = "fgh",
                    RepsOrTime = "jkl",
                    Sets = 1
                  }
                },
                Logs = new List<ExerciseLog>
                {
                  new ExerciseLog
                  {
                    TimeStamp = new DateTime(2000, 1, 1)
                  }
                }
              }
      };

      foreach (var wp in WorkoutPrograms)
      {
        repo.Insert(wp);
      }

    }
  }
}
