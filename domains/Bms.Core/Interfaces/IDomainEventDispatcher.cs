using BMS.Core.SharedKernel;

namespace BMS.Core.Interfaces
{
  public interface IDomainEventDispatcher
  {
    void Dispatch(BaseDomainEvent domainEvent);
  }
}
