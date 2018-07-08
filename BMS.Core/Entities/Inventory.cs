using BMS.Core.SharedKernel;
using System.Collections.Generic;

namespace BMS.Core.Entities
{
  public class Inventory : BaseEntity
  {
    public PartDetails Part { get; set; }
    public Warehouse Warehouse { get; set; }
    public string QRCode { get; set; }
    public string Description { get; set; }
    public int TotalQuantity { get; set; }
    public IList<InventoryLocation> InventoryLocations { get; set; }
  }
}
