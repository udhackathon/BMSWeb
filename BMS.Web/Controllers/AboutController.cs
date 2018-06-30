using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.Core.Entities;
using BMS.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bms.Controllers
{
  [Produces("application/json")]
  [Route("api/About")]
  public class AboutController : Controller
  {
    private readonly IRepository<Inventory> _inventoryRepository;

    public AboutController(IRepository<Inventory> inventoryRepository)
    {
      _inventoryRepository = inventoryRepository;
    }

    [HttpGet]
    public string GetAbout()
    {
      return "UD Truck Hackathon - Buffer Managemenet System - " + DateTime.Now.ToLongDateString().ToString();
    }

    //[HttpGet]
    //public IActionResult List()
    //{
    //  var items = _inventoryRepository.List();
    //                        //.Select(item => ToDoItemDTO.FromToDoItem(item));
    //  return Ok(items);
    //}
  }
}
