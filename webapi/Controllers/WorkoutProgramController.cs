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
    public IEnumerable<WorkoutProgram> Get()
    {
      return _repo.GetAll();
    }

    // GET: api/WorkoutProgram/5
    [HttpGet("{id}", Name = "Get")]
    public WorkoutProgram Get(string id)
    {
      return _repo.Get(id);
    }

    // POST: api/WorkoutProgram
    [HttpPost]
    public void Post([FromBody]WorkoutProgram value)
    {
      _repo.Insert(value);
    }

    // PUT: api/WorkoutProgram/5
    [HttpPut("{id}")]
    public void Put(string id, [FromBody]WorkoutProgram value)
    {
      var obj = _repo.Get(id);
      obj.Name = value.Name != null ? value.Name : obj.Name;
      obj.Logs = value.Logs != null ? value.Logs : obj.Logs;
      obj.ExerciseList = value.ExerciseList != null ? value.ExerciseList : obj.ExerciseList;
      _repo.Update(obj);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
      var obj = _repo.Get(id);
      if(obj != null)
      {
        _repo.Delete(obj);
      }
    }
  }
}
