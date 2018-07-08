using BMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BMS.Core.Entities;
using BMS.Core.SharedKernel;

namespace BMS.Infrastructure.Data
{
  public class AppDbContext : DbContext
  {
    private readonly IDomainEventDispatcher _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher)
        : base(options)
    {
      _dispatcher = dispatcher;
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<InventoryLocation> InventoryLocation { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<BinLocation> BinLocation { get; set; }
    public DbSet<PartDetails> PartDetails { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //  modelBuilder.Entity<Inventory>()
    //    .HasKey(k => new { k.Id, k.Warehouse });
    //}

    public override int SaveChanges()
    {
      int result = base.SaveChanges();

      // dispatch events only if save was successful
      var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
          .Select(e => e.Entity)
          .Where(e => e.Events.Any())
          .ToArray();

      foreach (var entity in entitiesWithEvents)
      {
        var events = entity.Events.ToArray();
        entity.Events.Clear();
        foreach (var domainEvent in events)
        {
          _dispatcher.Dispatch(domainEvent);
        }
      }

      return result;
    }
  }

  public static class EFHelper {
    public static IQueryable<T> IncludeSubset<T>(this IQueryable<T> query, params string[] navProperties)
    where T : class
    {
      foreach (var navProperty in navProperties)
        query = query.Include(navProperty);

      return query;
    }
  }
}
