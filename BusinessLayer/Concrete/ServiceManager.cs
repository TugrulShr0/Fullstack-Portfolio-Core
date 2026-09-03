namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ServiceManager(IServiceDal dal) : IServiceService
{
    public void TAdd(Service t) => dal.Insert(t);
    public void TDelete(Service t) => dal.Delete(t);
    public void TUpdate(Service t) => dal.Update(t);
    public List<Service> TGetList() => dal.GetList();
    public Service? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Service t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Service t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Service t) => await dal.UpdateAsync(t);
    public async Task<List<Service>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Service?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
