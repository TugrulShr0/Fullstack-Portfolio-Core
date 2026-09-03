namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ExperienceManager(IExperienceDal dal) : IExperienceService
{
    public void TAdd(Experience t) => dal.Insert(t);
    public void TDelete(Experience t) => dal.Delete(t);
    public void TUpdate(Experience t) => dal.Update(t);
    public List<Experience> TGetList() => dal.GetList();
    public Experience? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Experience t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Experience t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Experience t) => await dal.UpdateAsync(t);
    public async Task<List<Experience>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Experience?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
