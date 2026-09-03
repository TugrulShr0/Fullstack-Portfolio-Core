namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AboutManager(IAboutDal dal) : IAboutService
{
    public void TAdd(About t) => dal.Insert(t);
    public void TDelete(About t) => dal.Delete(t);
    public void TUpdate(About t) => dal.Update(t);
    public List<About> TGetList() => dal.GetList();
    public About? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(About t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(About t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(About t) => await dal.UpdateAsync(t);
    public async Task<List<About>> TGetListAsync() => await dal.GetListAsync();
    public async Task<About?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
