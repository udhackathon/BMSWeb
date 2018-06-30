using System;

namespace BMS.Core.SharedKernel
{
  public abstract class BaseDomainEvent
  {
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
  }
}
