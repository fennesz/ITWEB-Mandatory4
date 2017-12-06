using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL.repos;
using webapi.DAL.models;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers
{
  [Route("api/WorkoutProgram")]
  public class WorkoutProgramController : Controller
  {
    private WorkoutProgramRepo _repo;
    public WorkoutProgramController(WorkoutProgramRepo repo)
    {
      _repo = repo;
    }

    // GET: api/WorkoutProgram
    [HttpGet]
    public ActionResult Get()
    {
      var data = _repo.GetAll().ToList();
      foreach (var wp in data)
      {
        RemoveCircularReferencesFromWorkoutProgram(wp);
      }
      return Json(data);
    }

    // GET: api/WorkoutProgram/5
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
      var data = _repo.Get(id);
      RemoveCircularReferencesFromWorkoutProgram(data);
      return Json(data);
    }

    // POST: api/WorkoutProgram
    [HttpPost]
    [Authorize]
    public ActionResult Post()
    {
      var id = _repo.Insert(new WorkoutProgram())._id;
      var url = Url.Action("Get", "WorkoutProgram", new { id = id }, Request.Scheme);
      return Json(new { location = url });
    }

    // PUT: api/WorkoutProgram/5
    [HttpPatch("{id}")]
    [Authorize]
    public ActionResult Patch(string id, [FromBody]WorkoutProgram value)
    {
      var obj = _repo.Get(id);
      obj.Name = value.Name != null ? value.Name : obj.Name;
      obj.Logs = value.Logs != null ? value.Logs : obj.Logs;
      obj.ExerciseList = value.ExerciseList != null ? value.ExerciseList : obj.ExerciseList;
      _repo.Update(obj);
      RemoveCircularReferencesFromWorkoutProgram(obj);
      return Json(obj);
    }

    // PUT: api/WorkoutProgram/5
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult Put(string id, [FromBody]WorkoutProgram value)
    {
      var obj = _repo.Get(id);
      obj.Name = value.Name;
      obj.Logs = value.Logs;
      obj.ExerciseList = value.ExerciseList;
      _repo.Update(obj);
      RemoveCircularReferencesFromWorkoutProgram(obj);
      return Json(new { id = id, @object = obj });
    }

    // DELETE: api/WorkoutProgram/5
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(string id)
    {
      var obj = _repo.Get(id);
      if (obj != null)
      {
        _repo.Delete(obj);
      }
      return Json(new { });
    }

    // Dirty hacks to help json serialization, too lazy to actually make
    // proper DTOs
    private void RemoveCircularReferencesFromWorkoutProgram(WorkoutProgram wp)
    {
      if (wp.ExerciseList != null)
      {
        foreach (var ex in wp.ExerciseList)
        {
          ex.WorkoutProgram = null;
        }
      }
      if (wp.Logs != null)
      {
        foreach (var log in wp.Logs)
        {
          log.WorkoutProgram = null;
        }
      }
    }
  }
}
