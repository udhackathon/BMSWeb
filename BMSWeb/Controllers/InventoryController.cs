using BMS.Core.Interfaces;
using BMS.Infrastructure.Data;
using BMS.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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
    public InventoryController(IInventoryService inventoryService, AppDbContext dbContext, IConfiguration config)
    {
      _dbContext = dbContext;
      _inventoryService = inventoryService;
      Configuration = config;
    }

    [HttpPost("SeedMasterData{token}")]
    public IActionResult SeedMasterData(string token)
    {
      var usertoken = Configuration["usertoken"];
      if (token == usertoken)
      {
        SeedData.PopulateTestData(_dbContext);
        return Ok("Master data seeded successfully");
      }
      else
        return Ok("wrong token passed");
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

    [HttpGet("{QRCode}")]
    public IActionResult Get(string QRCode)
    {
      return Ok(_inventoryService.FindInventories(QRCode)); // return inventory filtered by qrCode
    }

    [HttpPost("GenerateSNP{token}")]
    public IActionResult GenerateSNP([FromBody]InventoryModel inventoryModel, string token)
    {
      var usertoken = Configuration["usertoken"];
      if (token == usertoken)
      {
        var inventory = _inventoryService.GenerateSNP(inventoryModel.WarehouseId, inventoryModel.PartId, inventoryModel.Quantity, inventoryModel.Remark);

        return Ok(inventory);
      }
      else
        return Ok("wrong token passed");
    }

    [HttpGet("GetPartInWearhouse")]
    public IActionResult GetPartInWearhouse(int wearhouseId)
    {
      var inventorys = _inventoryService.FindInventories(wearhouseId);

      IList<PartQuantityModel> models = new List<PartQuantityModel>();
      foreach (var inventory in inventorys)
      {
        var partQuantity = new PartQuantityModel()
        {
          PartName = inventory.Part.Name,
          Quantity = inventory.TotalQuantity
        };
        models.Add(partQuantity);
      }

      return Ok(models);
    }

    [HttpPost("UpdateLocation{token}")]
    public IActionResult UpdateLocation(string qrCode, int binLocationId, int quantity, string token)
    {
      var usertoken = Configuration["usertoken"];
      if (token == usertoken)
      {
        bool result = _inventoryService.UpdateLocation(qrCode, binLocationId, quantity);
        if (result)
          return Ok();
        else
          return BadRequest();
      }
      else
        return Ok("wrong token passed");
    }

    [HttpGet("GetThresholdNotification{wearhouseId}")]
    public IActionResult GetThresholdNotification(int wearhouseId)
    {
      IList<string> notifications = _inventoryService.CheckInventoryThreshold(wearhouseId);
      return Ok(notifications);
    }
    #endregion

    #region ToDo
    [HttpGet("GetSNPByInventoryId()")]
    public IActionResult GetSNP(int inventoryId)
    {
      return Ok(); // return SNP information
    }

    [HttpGet("GetPartscapacity{GetPartscapacity}")]
    public IActionResult GetPartscapacity(int warehouseId)
    {
      var model = new List<PartCapacityModel>();
      model.Add(new PartCapacityModel { PartNo = "OIL FILTER", Capacity = 100, Quantity = 30 });
      model.Add(new PartCapacityModel { PartNo = "FILTER KIT", Capacity = 90, Quantity = 100 });
      model.Add(new PartCapacityModel { PartNo = "SPRING", Capacity = 80, Quantity = 70 });
      model.Add(new PartCapacityModel { PartNo = "BUSHING", Capacity = 110, Quantity = 90 });
      model.Add(new PartCapacityModel { PartNo = "SEIBI KIT", Capacity = 130, Quantity = 60 });
      model.Add(new PartCapacityModel { PartNo = "DIAPHRAGM", Capacity = 70, Quantity = 50 });
      return Ok(model); 
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

    [HttpGet("GetParts{name}")]
    public IActionResult GetParts(string name)
    {
      return Ok(_inventoryService.GetParts(name)); //return all part
    }


    #endregion
  }
}
