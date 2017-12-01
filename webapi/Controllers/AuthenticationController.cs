using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
  [Produces("application/json")]
  [Route("api/Auth")]
  public class AuthenticationController : Controller
  {
    [HttpPost]
    public IActionResult Create(string username, string password)
    {
      return new ObjectResult(username);
      // if (IsValidUserAndPasswordCombination(username, password))
      //return new ObjectResult(GenerateToken(username));
      //  return BadRequest();
    }
  }
}
