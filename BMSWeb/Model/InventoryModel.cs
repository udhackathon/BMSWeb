namespace BMS.Web.Model
{
  public class InventoryModel
  {
    public int WarehouseId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
    public string Remark { get; set; }
  }

  public class UpdateInventoryModel
  {
    public string QRCode { get; set; }
    public int BinLocationId { get; set; }
    public int Quantity { get; set; }
  }
}
