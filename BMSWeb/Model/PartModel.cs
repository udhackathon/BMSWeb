namespace BMS.Web.Model
{
  public class PartModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string PartNo { get; set; }
  }
  public class PartCapacityModel
  {
    public string Name { get; set; }
    public string PartNo { get; set; }
    public int Quantity { get; set; }
    public int Capacity { get; set; }
  }
}
