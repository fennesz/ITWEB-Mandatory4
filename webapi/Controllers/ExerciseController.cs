using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL.repos;
using webapi.DAL.models;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers
{
  [Route("api/WorkoutProgram/{WPid}/Exercise")]
  public class ExerciseController : Controller
  {
    private WorkoutProgramRepo _repo;
    public ExerciseController(WorkoutProgramRepo repo)
    {
      _repo = repo;
    }

    // GET: api/workoutprogram/{WPid}/Exercise
    [HttpGet]
    public ActionResult Get(string WPid)
    {
      if (WPid == null) throw new ArgumentNullException(nameof(WPid));
      var data = _repo.Get(WPid).ExerciseList;
      foreach (var ex in data)
      {
        RemoveCircularReferencesFromExercises(ex);
      }
      return Json(data);
    }

    // GET: api/WorkoutProgram/{WPid}/Exercise/{id}
    [HttpGet("{id}")]
    public ActionResult Get(string WPid, string id)
    {
      var data = _repo.Get(WPid).ExerciseList.First(x => x._id == id);
      RemoveCircularReferencesFromExercises(data);
      return Json(data);
    }

    // POST: api/WorkoutProgram/{WPid}/Exercise
    [HttpPost]
    [Authorize]
    public ActionResult Post(string WPid)
    {
      var obj = _repo.Get(WPid);
      var guid = new Guid();
      obj.ExerciseList.Add(new Exercise {ExerciseName = guid.ToString() });
      obj = _repo.Update(obj);
      var id = obj.ExerciseList.First(x => x.ExerciseName == guid.ToString())._id; // Nasty nasty hacks
      var url = Url.Action("Get", "Exercise", new { WPid = WPid, id = id }, Request.Scheme);
      return Json(new { location = url });
    }

    // PUT: api/WorkoutProgram/{WPid}/Exercise/{id}
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult Put(string WPid, string id, [FromBody]Exercise value)
    {
      if(value == null) {
        return Json(new {err = "wrong data sent" });
      }
      var obj = _repo.Get(WPid);
      var list = obj.ExerciseList;
      var exerciseToUpdate = obj.ExerciseList.First(x => x._id == id);
      
      exerciseToUpdate.Description = value.Description;
      exerciseToUpdate.ExerciseName = value.ExerciseName;
      exerciseToUpdate.RepsOrTime = value.RepsOrTime;
      exerciseToUpdate.Sets = value.Sets;

      _repo.Update(obj);
      RemoveCircularReferencesFromExercises(exerciseToUpdate);
      return Json(exerciseToUpdate);
    }

    // DELETE: api/WorkoutProgram/{WPid}/Exercise/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(string WPid, string id)
    {
      var obj = _repo.Get(WPid);
      var exerciseToRemove = obj.ExerciseList.First(x => x._id == id);
      obj.ExerciseList.Remove(exerciseToRemove);

      _repo.Update(obj);
      return Json(new { });
    }

    private void RemoveCircularReferencesFromExercises(Exercise ex)
    {
      ex.WorkoutProgram = null;
    }
  }
}
