using BMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.Core.Interfaces
{
    public interface IInventoryService
    {
      IList<Inventory> FindInventories();
      IList<Inventory> FindInventories(int warehouseid);
      IList<Inventory> FindInventories(string QRCode, int WarehouseId);
      IList<PartDetails> AllPartsInWarehouse(int warehouseId);
      IList<Warehouse> GetWarehouse();
      IList<BinLocation> WarehouseBinLocations(int warehouseId);
      IList<Location> GetLocations();
  }
}
