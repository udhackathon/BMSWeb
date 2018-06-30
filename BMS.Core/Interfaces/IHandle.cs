using BMS.Core.SharedKernel;

namespace BMS.Core.Interfaces
{
  public interface IHandle<T> where T : BaseDomainEvent
  {
    void Handle(T domainEvent);
  }
}
