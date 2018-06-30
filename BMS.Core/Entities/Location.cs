using BMS.Core.Events;
using BMS.Core.SharedKernel;

namespace BMS.Core.Entities
{
  public class Location : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Deleted { get; private set; } = false;
  }
}
