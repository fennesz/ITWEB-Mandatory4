using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace webapi.Controllers
{
  public class HomeController : Controller
  {
    private IHostingEnvironment _environment;
    public HomeController(IHostingEnvironment environment)
    {
      _environment = environment;
    }

    public IActionResult Index()
    {
      return new RedirectResult("~/");
    }
  }
}
