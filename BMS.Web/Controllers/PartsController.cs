using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.Core.Entities;
using BMS.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/parts")]
  public class PartsController : Controller
  {
    public PartsService _partsService;
    public PartsController(PartsService partsService)
    {
      _partsService = partsService;
    }

    // GET: Parts
    [HttpGet("Index")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("Details")]
    // GET: Parts/Details/5
    public Parts Details()
    {
      return _partsService.getPartInfo();
    }
  }
}
