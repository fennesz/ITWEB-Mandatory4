using System;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL.repos;
using webapi.DAL.models;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers
{
  [Route("api/WorkoutProgram/{WPid}/Logs")]
  public class ExerciseLogController : Controller
  {
    private WorkoutProgramRepo _repo;
    public ExerciseLogController(WorkoutProgramRepo repo)
    {
      _repo = repo;
    }


    // Get: api/WorkoutProgram/{WPid}/log
    [HttpGet]
    public ActionResult Get(string WPid)
    {
      if (WPid == null) throw new ArgumentNullException(nameof(WPid));
      var data = _repo.Get(WPid).Logs;
      foreach (var log in data)
      {
        RemoveCircularReferencesFromLog(log);
      }
      return Json(data);
    }

    // POST: api/WorkoutProgram/{WPid}/log
    [HttpPost]
    [Authorize]
    public ActionResult Post(string WPid)
    {
      var wp = _repo.Get(WPid);

      var exerciseLog = new ExerciseLog
      {
        TimeStamp = DateTime.Now
      };
      wp.Logs.Add(exerciseLog);
      _repo.Update(wp);
      RemoveCircularReferencesFromLog(exerciseLog);
      return Json(new { id = exerciseLog._id, data = exerciseLog });
    }

    private void RemoveCircularReferencesFromLog(ExerciseLog el)
    {
      el.WorkoutProgram = null;
    }
  }
}
