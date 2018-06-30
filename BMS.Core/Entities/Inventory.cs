using BMS.Core.Events;
using BMS.Core.SharedKernel;

namespace BMS.Core.Entities
{
  public class Inventory : BaseEntity
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; private set; } = false;

    public void MarkComplete()
    {
      IsDone = true;
      Events.Add(new InventoryAddedEvent(this));
    }
  }
}
