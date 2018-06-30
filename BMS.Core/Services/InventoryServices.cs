//using Ardalis.GuardClauses;
using BMS.Core.Events;
using BMS.Core.Interfaces;

namespace BMS.Core.Services
{
  public class InventoryService : IHandle<InventoryAddedEvent>
  {
    public void Handle(InventoryAddedEvent domainEvent)
    {
      //Guard.Against.Null(domainEvent, nameof(domainEvent));

      // Do Nothing
    }
  }
}
