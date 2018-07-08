//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BMS.Core.Entities;
//using BMS.Core.Interfaces;
//using BMS.Infrastructure.Data;
//using BMS.Web;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace bms.Controllers
//{
//  [Produces("application/json")]
//  [Route("api/About")]
//  public class AboutController : Controller
//  {
//    private readonly IRepository<Inventory> _inventoryRepository;
//    private readonly IRepository<Location> _locationRepository;
//    private readonly AppDbContext _dbContext;

//    public AboutController(IRepository<Inventory> inventoryRepository,AppDbContext dbContext, IRepository<Location> locationRepository)
//    {
//      _dbContext = dbContext;
//      _inventoryRepository = inventoryRepository;
//      _locationRepository = locationRepository;
//    }

//    [HttpGet]
//    public string GetAbout()
//    {
//      return "UD Truck Hackathon - Buffer Managemenet System - " + DateTime.Now.ToLongDateString().ToString();
//    }

//    [HttpGet("SeedMasterData")]
//    public IActionResult SeedMasterData()
//    {
//      SeedData.PopulateTestData(_dbContext);
//      return Ok();
//    }

//    [HttpGet("GetLocations")]
//    public IActionResult GetLocations()
//    {
//      var item = _locationRepository.List();
//      return Ok(item);
//    }
//    //[HttpGet]
//    //public IActionResult List()
//    //{
//    //  var items = _inventoryRepository.List();
//    //                        //.Select(item => ToDoItemDTO.FromToDoItem(item));
//    //  return Ok(items);
//    //}
//  }
//}
