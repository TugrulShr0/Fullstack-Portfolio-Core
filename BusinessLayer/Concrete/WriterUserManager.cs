namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WriterUserManager(IWriterUserDal dal) : IWriterUserService
{
    public void TAdd(WriterUser t) => dal.Insert(t);
    public void TDelete(WriterUser t) => dal.Delete(t);
    public void TUpdate(WriterUser t) => dal.Update(t);
    public List<WriterUser> TGetList() => dal.GetList();
    public WriterUser? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(WriterUser t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(WriterUser t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(WriterUser t) => await dal.UpdateAsync(t);
    public async Task<List<WriterUser>> TGetListAsync() => await dal.GetListAsync();
    public async Task<WriterUser?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
