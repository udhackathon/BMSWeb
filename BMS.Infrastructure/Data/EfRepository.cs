using System.Collections.Generic;
using System.Linq;
using BMS.Core.Interfaces;
using BMS.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BMS.Infrastructure.Data
{
  public class EfRepository<T> : IRepository<T> where T : BaseEntity
  {
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public T GetById(int id)
    {
      return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
    }

    public List<T> List()
    {
      return _dbContext.Set<T>().ToList();
    }

    public T Add(T entity)
    {
      _dbContext.Set<T>().Add(entity);
      _dbContext.SaveChanges();

      return entity;
    }

    public void Delete(T entity)
    {
      _dbContext.Set<T>().Remove(entity);
      _dbContext.SaveChanges();
    }

    public void Update(T entity)
    {
      _dbContext.Entry(entity).State = EntityState.Modified;
      _dbContext.SaveChanges();
    }
    public IQueryable<T> ListQuery(string[] includedNavigationProperties)
    {
      return _dbContext.Set<T>().IncludeSubset(includedNavigationProperties);
    }
  }
}
