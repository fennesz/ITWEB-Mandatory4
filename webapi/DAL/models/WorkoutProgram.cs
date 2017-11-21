using System.Collections.Generic;

namespace webapi.DAL.models
{
  public class WorkoutProgram
  {
    public string _id { get; set; }

    public string Name { get; set; }
    public ICollection<Exercise> ExerciseList { get; set; }
    public ICollection<ExerciseLog> Logs { get; set; }
  }
}
