using BMS.Core.Events;
using BMS.Core.SharedKernel;
using System.Collections.Generic;

namespace BMS.Core.Entities
{
  public class Inventory : BaseEntity
  {
    public PartDetails partDetails { get; set; }
    public string Description { get; set; }
    public int TotalQuantity { get; set; }
    public IList<BinLocation> BinLocations { get; set; }
  }
}
