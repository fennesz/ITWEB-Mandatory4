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
  [Route("api/Register")]
  public class RegisterController : Controller
  {

    private UserRepo _repo;
    public RegisterController(UserRepo repo)
    {
      _repo = repo;
    }


    // POST: api/Register
    [HttpPost]
    public void Post([FromBody]User value)
    {
      _repo.Insert(value);
    }
  }
}
