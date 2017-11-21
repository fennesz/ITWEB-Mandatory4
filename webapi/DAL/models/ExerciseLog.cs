using System;

namespace webapi.DAL.models
{
  public class ExerciseLog
  {
    public string _id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string WorkoutProgramId { get; set; }
    public WorkoutProgram WorkoutProgram { get; set; }
  }
}
