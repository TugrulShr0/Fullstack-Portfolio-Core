namespace BusinessLayer.Abstract;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IGenericService<T>
{
    Task TAddAsync(T t);
    Task TDeleteAsync(T t);
    Task TUpdateAsync(T t);
    Task<List<T>> TGetListAsync();
    Task<T?> TGetByIDAsync(int id);

    void TAdd(T t);
    void TDelete(T t);
    void TUpdate(T t);
    List<T> TGetList();
    T? TGetByID(int id);
}
