using BMS.Core.Entities;
using System;
using System.Collections.Generic;

namespace BMS.Core.SharedKernel
{
  // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
  public abstract class BaseEntity
  {
    public int Id { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.Now;

    public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
  }
}
