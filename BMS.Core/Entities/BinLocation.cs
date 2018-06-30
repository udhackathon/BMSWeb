using BMS.Core.Events;
using BMS.Core.SharedKernel;

namespace BMS.Core.Entities
{
  public class BinLocation : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; } 
    public BinningType BinType  { get; set; }
    public Warehouse Warehouse { get; set; }
  }
}
