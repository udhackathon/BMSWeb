using BMS.Core.Events;
using BMS.Core.SharedKernel;

namespace BMS.Core.Entities
{
  public class User : BaseEntity
  {
    public string Name { get; set; }
    public Role Role { get; set; }
  }
}
