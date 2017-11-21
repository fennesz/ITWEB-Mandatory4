namespace webapi.DAL.models
{
  public class Exercise
  {
    public string _id { get; set; }

    public string ExerciseName { get; set; }
    public string Description { get; set; }
    public int Sets { get; set; }
    public string RepsOrTime { get; set; }
    public string WorkoutProgramId { get; set; }
    public WorkoutProgram WorkoutProgram { get; set; }
  }
}
