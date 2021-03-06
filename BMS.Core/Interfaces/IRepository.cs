using System.Collections.Generic;
using System.Linq;
using BMS.Core.SharedKernel;

namespace BMS.Core.Interfaces
{
  public interface IRepository<T> where T : BaseEntity
  {
    T GetById(int id);
    List<T> List();
    T Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> ListQuery(string[] includedNavigationProperties);
  }
}
