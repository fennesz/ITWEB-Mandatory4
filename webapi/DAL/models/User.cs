namespace webapi.DAL.models
{
  public class User
  {
    public string _id { get; set; }

    public string Email { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }
    public string Salt { get; set; }
  }
}
