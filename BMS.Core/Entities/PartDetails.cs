using BMS.Core.SharedKernel;
using System.Collections.Generic;

namespace BMS.Core.Entities
{
  public class PartDetails : BaseEntity
  {
    public string Name { get; set; }
  }
  public class Parts
  {
    public string Id { get; set; }
    public string WarehouseLoc { get; set; }
    public string PartName { get; set; }
    public string Country { get; set; }
    public Primary_Inventory primary_Inventory { get; set; } = new Primary_Inventory();
    public IList<Buffer_Inventory> buffer_inventory { get; set; } = new List<Buffer_Inventory>();
  }
  public class Dashboard
  {
    public int Total_count { get; set; }
    public Primary_Inventory primary_Inventory { get; set; }
    public Buffer_Inventory buffer_Inventory { get; set; }
  }
  public class Primary_Inventory
  {
    public int? max_count { get; set; }
    public int? current_count { get; set; }
  }
  public class Buffer_Inventory
  {
    public int Id { get; set; }
    public int? max_count { get; set; }
    public int? current_count { get; set; }
  }
}
