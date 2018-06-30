using BMS.Core.Entities;
using BMS.Core.SharedKernel;

namespace BMS.Core.Events
{
  public class InventoryAddedEvent : BaseDomainEvent
  {
    public Inventory Inventory { get; set; }

    public InventoryAddedEvent(Inventory inventory)
    {
      Inventory = inventory;
    }
  }
}
