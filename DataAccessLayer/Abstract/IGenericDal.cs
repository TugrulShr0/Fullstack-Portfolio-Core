namespace DataAccessLayer.Abstract;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IGenericDal<T> where T : class
{
    Task InsertAsync(T t);
    Task UpdateAsync(T t);
    Task DeleteAsync(T t);
    Task<List<T>> GetListAsync();
    Task<T?> GetByIDAsync(int id);
    Task<List<T>> GetbyFilterAsync(Expression<Func<T, bool>> filter);

    void Insert(T t);
    void Update(T t);
    void Delete(T t);
    List<T> GetList();
    T? GetByID(int id);
    List<T> GetbyFilter(Expression<Func<T, bool>> filter);
}
