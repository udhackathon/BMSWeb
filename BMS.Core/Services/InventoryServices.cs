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
      string[] includedNavigationProperties = new string[] { "Warehouse", "Part", "InventoryLocations", "InventoryLocations.BinLocation" };

      var inventories = _inventoryRepository.ListQuery(includedNavigationProperties);
      return inventories.ToList();
    }

    public IList<Inventory> FindInventories(int warehouseid)
    {
      string[] includedNavigationProperties = new string[] { "Warehouse","Part", "InventoryLocations", "InventoryLocations.BinLocation" };

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

    public bool UpdateLocation(string qrCode, int binLocationId, int quantity)
    {
      bool result = false;

      string[] includedNavigationProperties = new string[] { "InventoryLocations", "InventoryLocations.BinLocation" };
      var inventories = _inventoryRepository.ListQuery(includedNavigationProperties);
      var inventory = inventories.FirstOrDefault(i => i.QRCode == qrCode);
      if (inventory != null)
      {        
        if (inventory.InventoryLocations != null && inventory.InventoryLocations.Any())
        {
          var inventoryLoc = inventory.InventoryLocations.FirstOrDefault(f => f.BinLocation != null && f.BinLocation.Id == binLocationId);
          if (inventoryLoc == null)
          {
            if (quantity > 0) //if quantity is greater than zero add InventoryLocation
            {
              var inventoryLocation = new InventoryLocation()
              {
                BinLocation = _binLocationRepository.GetById(binLocationId),
                Quantity = quantity
              };
              inventory.InventoryLocations.Add(inventoryLocation);

              _inventoryRepository.Update(inventory);
              result = true;
            }
          }
          else
          {
            //if quantity is zero remove InventoryLocation for given bin location
            if (quantity == 0)
            {
              inventory.InventoryLocations.Remove(inventoryLoc);
              _inventoryRepository.Update(inventory);
            }
            else
            {
              //update quantity of InventoryLocation with given bin location
              inventoryLoc.Quantity = quantity;
              _inventoryRepository.Update(inventory);
            }
            result = true;
          }
        }
      }

      return result;
    }

    public IList<string> CheckInventoryThreshold(int wearhouseId)
    {
      IList<string> notifications = new List<string>();

      var inventories = this.FindInventories(wearhouseId);
      foreach (var inventory in inventories)
      {
        if(inventory.InventoryLocations != null && inventory.InventoryLocations.Count > 0)
        {
          var primeInventoryLocation = inventory.InventoryLocations.FirstOrDefault(w => w.BinLocation != null && w.BinLocation.BinType == BinningType.Prime);
          if (primeInventoryLocation != null && primeInventoryLocation.Quantity < primeInventoryLocation.BinLocation.Capacity)
          {
            var bufferInventoryLocations = inventory.InventoryLocations.Where(w => w.BinLocation != null && w.BinLocation.BinType == BinningType.Buffer);
            foreach (var bufferInventoryLocation in bufferInventoryLocations)
            {
              if(bufferInventoryLocation.Quantity > 0)
              {
                string notification = string.Format("Prime location {0} has space.", primeInventoryLocation.BinLocation.Name);
                notifications.Add(notification);
                break;
              }
            }
          }
        }
      }

      return notifications;
    }

    public IList<PartDetails> GetParts(string name)
    {
      string[] includedNavigationProperties = new string[] {  };
      var part1 = _partDetailsRepository.ListQuery(includedNavigationProperties).Where(i => i.Name.Contains(name.ToUpper())).ToList();
      var part2 = _partDetailsRepository.ListQuery(includedNavigationProperties).Where(i => i.PartNo.Contains(name.ToUpper())).ToList();
      return part1.Concat(part2).ToList();
    }

    public Inventory GetSNPByPartId(int partId)
    {
      string[] includedNavigationProperties = new string[] { "Part", "InventoryLocations", "InventoryLocations.BinLocation" };
      var inventoryQuery = _inventoryRepository.ListQuery(includedNavigationProperties).Where(i=>i.Part.Id == partId);
      return inventoryQuery.FirstOrDefault();
    }

    public Inventory GetInventoryByPartNo(string partNo)
    {
      string[] includedNavigationProperties = new string[] { "Part", "InventoryLocations", "InventoryLocations.BinLocation" };
      var inventoryQuery = _inventoryRepository.ListQuery(includedNavigationProperties).Where(i => i.Part.PartNo == partNo.ToUpper());
      return inventoryQuery.FirstOrDefault();
    }
  }
}
