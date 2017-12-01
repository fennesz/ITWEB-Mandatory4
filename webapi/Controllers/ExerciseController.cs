using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL.repos;
using webapi.DAL.models;

namespace webapi.Controllers
{
  [Produces("application/json")]
  [Route("api/WorkoutProgram/{WPid}/Exercise")]
  public class ExerciseController : Controller
  {
    private WorkoutProgramRepo _repo;
    public ExerciseController(WorkoutProgramRepo repo)
    {
      _repo = repo;
    }

    // GET: api/workoutprogram/id/Exercise
    [HttpGet]
    public IEnumerable<Exercise> Get(string WPid)
    {
      if (WPid == null) throw new ArgumentNullException(nameof(WPid));
      return _repo.Get(WPid).ExerciseList;
    }

    // GET: api/WorkoutProgram/id/Exercise/id
    [HttpGet("{id}", Name = "Get")]
    public ICollection<Exercise> Get(string WPid, string id)
    {
      return _repo.Get(id).ExerciseList;
    }

    // POST: api/WorkoutProgram/id/Exercise
    [HttpPost("{id}")]
    public void Post(string WPid, [FromBody]Exercise value)
    {
      var obj = _repo.Get(WPid);
      var list = obj.ExerciseList;
      list.Add(value);
      _repo.Update(obj);

    }

    // PUT: api/WorkoutProgram/id/Exercise/5
    [HttpPut("{id}")]
    public void Put(string WPid, string id, [FromBody]Exercise value)
    {
      var obj = _repo.Get(WPid);
      var list = obj.ExerciseList;
      var exerciseToUpdate = obj.ExerciseList.First(x => x._id == id);

      exerciseToUpdate._id = value._id;
      exerciseToUpdate.Description = value.Description;
      exerciseToUpdate.ExerciseName = value.ExerciseName;
      exerciseToUpdate.RepsOrTime = value.RepsOrTime;
      exerciseToUpdate.Sets = value.Sets;

      _repo.Update(obj);
    }

    // DELETE: api/WorkoutProgram/id/5
    [HttpDelete("{id}")]
    public void Delete(string WPid, string id)
    {
      var obj = _repo.Get(WPid);
      var exerciseToRemove = obj.ExerciseList.First(x => x._id == id);
      obj.ExerciseList.Remove(exerciseToRemove);

      _repo.Update(obj);

    }
  }
}
