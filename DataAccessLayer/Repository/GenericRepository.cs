namespace DataAccessLayer.Repository;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class GenericRepository<T> : IGenericDal<T> where T : class
{
    private readonly Context _context;

    public GenericRepository(Context context)
    {
        _context = context;
    }

    public GenericRepository()
    {
        _context = new Context();
    }

    public async Task DeleteAsync(T t)
    {
        _context.Remove(t);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetbyFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _context.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<T?> GetByIDAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetListAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task InsertAsync(T t)
    {
        await _context.AddAsync(t);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T t)
    {
        _context.Update(t);
        await _context.SaveChangesAsync();
    }

    public void Delete(T t)
    {
        _context.Remove(t);
        _context.SaveChanges();
    }

    public List<T> GetbyFilter(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().Where(filter).ToList();
    }

    public T? GetByID(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public List<T> GetList()
    {
        return _context.Set<T>().ToList();
    }

    public void Insert(T t)
    {
        _context.Add(t);
        _context.SaveChanges();
    }

    public void Update(T t)
    {
        _context.Update(t);
        _context.SaveChanges();
    }
}
