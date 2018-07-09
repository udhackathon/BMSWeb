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
    private readonly IRepository<PartDetails> _partDetailsRepository;

    public InventoryService(IRepository<Inventory> inventoryRepository, IRepository<Location> locationRepository,
      IRepository<Warehouse> warehouseRepository, IRepository<BinLocation> binLocationRepository,
      IRepository<PartDetails> partDetailsRepository)
    {
      _inventoryRepository = inventoryRepository;
      _locationRepository = locationRepository;
      _warehouseRepository = warehouseRepository;
      _binLocationRepository = binLocationRepository;
      _partDetailsRepository = partDetailsRepository;
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

    public IQueryable<Inventory> FindInventoriesQuery()
    {
      string[] includedNavigationProperties = new string[] { "Warehouse", "Part", "InventoryLocations" };
      return _inventoryRepository.ListQuery(includedNavigationProperties);
    }
    public IList<Inventory> FindInventories(string QRCode)
    {
      var inventories = FindInventoriesQuery();
      return inventories.Where(i => i.QRCode == QRCode).ToList();
    }

    public Inventory GenerateSNP(int warehouseId, int partId, int quantity,string description)
    {
      var inventoriesQuery = FindInventoriesQuery();
      var inventories = inventoriesQuery.Where(i => i.Part.Id == partId && i.Warehouse.Id == warehouseId).ToList();
      if (inventories == null || !inventories.Any())
        return NewSNP(warehouseId, partId, quantity, description);
      else
        return inventories.First(); 
    }

    private Inventory NewSNP(int warehouseId, int partId, int quantity,string description)
    {
      var part = _partDetailsRepository.GetById(partId);
      var qrcode = GenerateQrCode(warehouseId,part.PartNo);
      var warehouse = _warehouseRepository.GetById(warehouseId);
      
      var inventory = new Inventory
      {
        QRCode = qrcode,
        Warehouse = warehouse,
        Description = description,
        TotalQuantity = quantity,
        Part = part,
        CreatedOn = DateTime.Now
      };
      _inventoryRepository.Add(inventory);
      return inventory;
    }

    private string GenerateQrCode(int warehouseId, string partNo)
    {
      return "Q" + warehouseId.ToString() + partNo + DateTime.Now.ToString("yyyymmdd");
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
