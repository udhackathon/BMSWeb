using BMS.Core.Interfaces;
using BMS.Infrastructure.Data;
using BMS.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BMS.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/Inventory")]
  public class InventoryController : Controller
  {
    #region Common
    public IInventoryService _inventoryService;
    private readonly AppDbContext _dbContext;
    public IConfiguration Configuration { get; set; }
    public InventoryController(IInventoryService inventoryService,AppDbContext dbContext, IConfiguration config)
    {
      _dbContext = dbContext;
      _inventoryService = inventoryService;
      Configuration = config;
    }

    [HttpPost("SeedMasterData{token}")]
    public IActionResult SeedMasterData(string token)
    {
      if (token == Configuration["usertoken"])
      {
        SeedData.PopulateTestData(_dbContext);
        return Ok();
      }
      else
        return Forbid();
    }

    [HttpGet("About")]
    public string About()
    {
      return "UD Truck Hackathon - Buffer Managemenet System";
    }
    #endregion

    #region Inventory
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_inventoryService.FindInventories()); //return all inventory
    }

    [HttpGet("{WarehouseId}")]
    public IActionResult Get(int WarehouseId)
    {
      return Ok(_inventoryService.FindInventories(WarehouseId)); //return all inventory
    }


    [HttpGet("{QRCode},{WarehouseId}")]
    public IActionResult Get(string QRCode, int WarehouseId)
    {
      return Ok(_inventoryService.FindInventories(QRCode, WarehouseId)); // return inventory filtered by qrCode
    }

    #endregion

    #region ToDo
    [HttpGet("GetSNPByInventoryId")]
    public IActionResult GetSNP(int inventoryId)
    {
      return Ok(); // return SNP information
    }

    [HttpPost("GenerateSNP")]
    public IActionResult GenerateSNP([FromBody]InventoryModel inventoryModel)
    {
      return Ok();
    }

    [HttpPost("InventoryUpdateQuantity")]
    public IActionResult InventoryUpdateQuantity([FromBody]InventoryModel inventoryModel)
    {
      return Ok();
    }
    #endregion
    
    #region Location
    [HttpGet("GetLocations")]
    public IActionResult GetLocations()
    {
      return Ok(_inventoryService.GetLocations());
    }

    #endregion

    #region Warehouse
    [HttpGet("GetWarehouse")]
    public IActionResult GetWarehouse()
    {
      return Ok(_inventoryService.GetWarehouse());
    }

    #endregion

    #region BinLocation
    [HttpGet("WarehouseBinLocations{WarehouseId}")]
    public IActionResult WarehouseBinLocations(int WarehouseId)
    {
      return Ok(_inventoryService.WarehouseBinLocations(WarehouseId));
    }

    #endregion

    #region PartDetails

    [HttpGet("AllPartsInWarehouse{WarehouseId}")]
    public IActionResult AllPartsInWarehouse(int WarehouseId)
    {
      return Ok(_inventoryService.AllPartsInWarehouse(WarehouseId)); //return all inventory
    }
    #endregion
  }
}
