using BMS.Core.Events;
using BMS.Core.SharedKernel;

namespace BMS.Core.Entities
{
  public class InventoryLocation : BaseEntity
  {
    public BinLocation BinLocation { get; set; }
    public int Quantity { get; set; }
  }
}
