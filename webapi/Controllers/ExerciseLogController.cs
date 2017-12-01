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
  [Route("api/WorkoutProgram/{WPid}/ExerciseLog")]
  public class ExerciseLogController : Controller
  {
    private WorkoutProgramRepo _repo;
    public ExerciseLogController(WorkoutProgramRepo repo)
    {
      _repo = repo;
    }


    // Get: api/WorkoutProgram/id/ExerciseLog
    [HttpGet]
    public IEnumerable<ExerciseLog> Get(string WPid)
    {
      if (WPid == null) throw new ArgumentNullException(nameof(WPid));
      return _repo.Get(WPid).Logs;
    }

    // POST: api/WorkoutProgram/id/ExerciseLog
    [HttpPost("{id}")]
    public void Post(string WPid)
    {
      var wp = _repo.Get(WPid);

      var exerciseLog = new ExerciseLog();
      exerciseLog.TimeStamp = DateTime.Now;
      exerciseLog.WorkoutProgramId = WPid;
      
      wp.Logs.Add(exerciseLog);
      _repo.Update(wp);
    }

  }
}
