namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ContactManager(IContactDal dal) : IContactService
{
    public void TAdd(Contact t) => dal.Insert(t);
    public void TDelete(Contact t) => dal.Delete(t);
    public void TUpdate(Contact t) => dal.Update(t);
    public List<Contact> TGetList() => dal.GetList();
    public Contact? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Contact t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Contact t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Contact t) => await dal.UpdateAsync(t);
    public async Task<List<Contact>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Contact?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
