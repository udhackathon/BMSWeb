using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.Core.Entities;
using BMS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/dashboard")]
  public class DashboardController : Controller
    {
    public PartsService _partsService;
    public DashboardController()
    {
      _partsService = new PartsService();
    }
    public IActionResult Index()
        {
            return View();
        }
    [HttpGet]
    public Dashboard Details()
    {
      return _partsService.getDashboardInfo();
    }
    }
}
