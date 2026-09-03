namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AnnouncementManager(IAnnouncementDal dal) : IAnnouncementService
{
    public void TAdd(Announcement t) => dal.Insert(t);
    public void TDelete(Announcement t) => dal.Delete(t);
    public void TUpdate(Announcement t) => dal.Update(t);
    public List<Announcement> TGetList() => dal.GetList();
    public Announcement? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Announcement t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Announcement t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Announcement t) => await dal.UpdateAsync(t);
    public async Task<List<Announcement>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Announcement?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
