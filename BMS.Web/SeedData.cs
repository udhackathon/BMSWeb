using BMS.Core.Entities;
using BMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Web
{
  public static class SeedData
  {
    public static void PopulateTestData(AppDbContext dbContext)
    {
      var locations = dbContext.Location;
      foreach (var loc in locations)
      {
        dbContext.Remove(loc);
      }
      dbContext.SaveChanges();
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
