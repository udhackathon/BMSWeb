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
      IList<Inventory> FindInventories(string QRCode);
      IList<PartDetails> AllPartsInWarehouse(int warehouseId);
      IList<Warehouse> GetWarehouse();
      IList<BinLocation> WarehouseBinLocations(int warehouseId);
      IList<Location> GetLocations();
      Inventory GenerateSNP(int warehouseId, int partId, int quantity,string description);
      bool UpdateLocation(string qrCode, int binLocationId, int quantity, string movementType);
      IList<string> CheckInventoryThreshold(int wearhouseId);
      IList<PartDetails> GetParts(string name);
      Inventory GetSNPByPartId(int partId);
      Inventory GetInventoryByPartNo(string partNo);
  }
}
