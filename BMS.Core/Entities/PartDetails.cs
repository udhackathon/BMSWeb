using BMS.Core.SharedKernel;
using System.Collections.Generic;

namespace BMS.Core.Entities
{
  public class PartDetails : BaseEntity
  {
    public string Name { get; set; }
    public string PartNo { get; set; }
  }
}
