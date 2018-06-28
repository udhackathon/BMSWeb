using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bms.Controllers
{
    [Produces("application/json")]
    [Route("api/About")]
    public class AboutController : Controller
    {
    [HttpGet]
    public string Get()
    {
      return "UD Truck Hackathon - Buffer Managemenet System";
    }
  }
}
