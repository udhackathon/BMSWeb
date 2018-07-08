using System;
using System.Collections.Generic;
using System.Linq;
using BMS.Core.Entities;
using BMS.Core.Events;
using BMS.Core.Interfaces;

namespace BMS.Core.Services
{
  public class InventoryService : IInventoryService
  {
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<Location> _locationRepository;
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<BinLocation> _binLocationRepository;

    public InventoryService(IRepository<Inventory> inventoryRepository, IRepository<Location> locationRepository,
      IRepository<Warehouse> warehouseRepository, IRepository<BinLocation> binLocationRepository)
    {
      _inventoryRepository = inventoryRepository;
      _locationRepository = locationRepository;
      _warehouseRepository = warehouseRepository;
      _binLocationRepository = binLocationRepository;
    }

    public IList<PartDetails> AllPartsInWarehouse(int warehouseId)
    {
      string[] includedNavigationProperties = new string[] { "Part" };
      var inventoriesQuery = _inventoryRepository.ListQuery(includedNavigationProperties);
      var inventories = inventoriesQuery.Where(i => i.Warehouse.Id == warehouseId).ToList();
      var parts = new List<PartDetails>();
      foreach (var inventory in inventories)
      {
        parts.Add(inventory.Part);
      }
      return parts;
    }

    public IList<Inventory> FindInventories()
    {
      return _inventoryRepository.List();
    }

    public IList<Inventory> FindInventories(int warehouseid)
    {
      string[] includedNavigationProperties = new string[] { "Warehouse","Part", "InventoryLocations" };

      var inventories = _inventoryRepository.ListQuery(includedNavigationProperties);
      return inventories.Where(i=>i.Warehouse.Id==warehouseid).ToList();
    }

    public IList<Inventory> FindInventories(string QRCode, int WarehouseId)
    {
      string[] includedNavigationProperties = new string[] { "Warehouse", "Part", "InventoryLocations" };
      var inventories = _inventoryRepository.ListQuery(includedNavigationProperties);
      return inventories.Where(i => i.QRCode == QRCode && i.Warehouse.Id == WarehouseId).ToList();
    }

    public IList<Location> GetLocations()
    {
      return _locationRepository.List();
    }

    public IList<Warehouse> GetWarehouse()
    {
      return _warehouseRepository.List();
    }

    public IList<BinLocation> WarehouseBinLocations(int warehouseId)
    {
      string[] includedNavigationProperties = new string[] { "Warehouse" };
      return _binLocationRepository.ListQuery(includedNavigationProperties).Where(i=>i.Warehouse.Id == warehouseId).ToList();
    }
  }
}
