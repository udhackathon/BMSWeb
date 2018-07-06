namespace BMS.Web.Model
{
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
