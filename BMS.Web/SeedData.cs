using BMS.Core.Entities;
using BMS.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace BMS.Web
{
  public static class SeedData
  {
    public static void PopulateTestData(AppDbContext dbContext)
    {
      //Remove Transaction data
      foreach (var inv in dbContext.Inventory)
      {
        dbContext.Remove(inv);
      }
      foreach (var inventoryLoc in dbContext.InventoryLocation)
      {
        dbContext.Remove(inventoryLoc);
      }

      //Remove Master data
      foreach (var usr in dbContext.User)
      {
        dbContext.Remove(usr);
      }
      foreach (var location in dbContext.Location)
      {
        dbContext.Remove(location);
      }
      foreach (var wearHouse in dbContext.Warehouse)
      {
        dbContext.Remove(wearHouse);
      }
      foreach (var binloc in dbContext.BinLocation)
      {
        dbContext.Remove(binloc);
      }
      foreach (var partDetail in dbContext.PartDetails)
      {
        dbContext.Remove(partDetail);
      }

      //Save Changes
      dbContext.SaveChanges();

      //Add Master data
      var user1 = new User() { Id = 1, Name = "User1", Role = Role.Admin, CreatedOn = DateTime.Now };
      var user2 = new User() { Id = 2, Name = "User2", Role = Role.Admin, CreatedOn = DateTime.Now };
      dbContext.User.Add(user1);
      dbContext.User.Add(user2);
      
      //Add Master data
      var location1 = new Location() { Id = 1, Name = "Ageo", Description = "AgeoDescription", CreatedBy = user1, CreatedOn = DateTime.Now };
      var location2 = new Location() { Id = 2, Name = "India", Description = "IndiaDescription", CreatedBy = user1, CreatedOn = DateTime.Now };
      dbContext.Location.Add(location1);
      dbContext.Location.Add(location2);

      var warehouse1 = new Warehouse() { Id = 1, Name = "Wearhouse1", Description = "W1Description", Location = location1, CreatedBy = user1, CreatedOn = DateTime.Now };
      var warehouse2 = new Warehouse() { Id = 2, Name = "Wearhouse2", Description = "W2Description", Location = location2, CreatedBy = user1, CreatedOn = DateTime.Now };
      dbContext.Warehouse.Add(warehouse1);
      dbContext.Warehouse.Add(warehouse2);

      var binLocation1 = new BinLocation() { Id = 1, Name = "BinLoc1", Description = "BinLoc1Desc", BinType = BinningType.Prime, Warehouse = warehouse1, CreatedBy = user1, CreatedOn = DateTime.Now };
      var binLocation2 = new BinLocation() { Id = 2, Name = "BinLoc2", Description = "BinLoc2Desc", BinType = BinningType.Buffer, Warehouse = warehouse1, CreatedBy = user1, CreatedOn = DateTime.Now };
      dbContext.BinLocation.Add(binLocation1);
      dbContext.BinLocation.Add(binLocation2);

      var part1 = new PartDetails() { Id = 1, Name = "Part1", PartNo = "asd123", CreatedBy = user1, CreatedOn = DateTime.Now };
      var part2 = new PartDetails() { Id = 2, Name = "Part2", PartNo = "asd124", CreatedBy = user1, CreatedOn = DateTime.Now };
      var part3 = new PartDetails() { Id = 3, Name = "Part3", PartNo = "asd125", CreatedBy = user1, CreatedOn = DateTime.Now };
      dbContext.PartDetails.Add(part1);
      dbContext.PartDetails.Add(part2);
      dbContext.PartDetails.Add(part3);


      var inventoryLoc1 = new InventoryLocation() { Id = 1, BinLocation = binLocation1, Quantity = 50, CreatedBy = user1, CreatedOn = DateTime.Now }; 
      var inventoryLoc2 = new InventoryLocation() { Id = 2, BinLocation = binLocation2, Quantity = 50, CreatedBy = user1, CreatedOn = DateTime.Now }; 
      dbContext.InventoryLocation.Add(inventoryLoc1);
      dbContext.InventoryLocation.Add(inventoryLoc2);


      var inventory1 = new Inventory() { Id = 1, QRCode="QR420",Warehouse=warehouse1, Description = "Description1", TotalQuantity = 100, Part = part1, InventoryLocations = new List<InventoryLocation>() { inventoryLoc1 , inventoryLoc2}, CreatedBy = user1, CreatedOn = DateTime.Now }; 
      dbContext.Inventory.Add(inventory1);


      //Save Changes
      dbContext.SaveChanges();
    }
  }
}
