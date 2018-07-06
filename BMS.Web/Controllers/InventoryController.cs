using BMS.Core.Services;
using BMS.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/Inventory")]
  public class InventoryController : Controller
  {
    public InventoryService _inventoryService;
    public InventoryController(InventoryService inventoryService)
    {
      _inventoryService = inventoryService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(); //return all inventory
    }

    [HttpGet("GetInventoryByQRCode")]
    public IActionResult Get(string qrCode)
    {
      return Ok(); // return inventory filtered by qrCode
    }

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
  }
}
