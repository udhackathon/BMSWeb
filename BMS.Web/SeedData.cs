using BMS.Core.Entities;
using BMS.Infrastructure.Data;
using System;

namespace BMS.Web
{
  public static class SeedData
  {
    public static void PopulateTestData(AppDbContext dbContext)
    {

      //Remove Transaction data


      //Remove Master data
      var locations = dbContext.Location;

      foreach (var loc in locations)
      {
        dbContext.Remove(loc);
      }
      dbContext.SaveChanges();

      //Add Master data
      dbContext.Location.Add(new Location()
      {
        Id = 1,
        CreatedOn = DateTime.Now,
        Name = "Ageo"
      });
      dbContext.SaveChanges();
    }
  }
}
