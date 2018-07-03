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
    public DashboardController(PartsService partsService)
    {
      _partsService = partsService;
    }
    [HttpGet("Index")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("Details")]
    public Dashboard Details()
    {
      return _partsService.getDashboardInfo();
    }
  }
}
