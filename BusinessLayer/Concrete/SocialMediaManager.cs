namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SocialMediaManager(ISocialMediaDal dal) : ISocialMediaService
{
    public void TAdd(SocialMedia t) => dal.Insert(t);
    public void TDelete(SocialMedia t) => dal.Delete(t);
    public void TUpdate(SocialMedia t) => dal.Update(t);
    public List<SocialMedia> TGetList() => dal.GetList();
    public SocialMedia? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(SocialMedia t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(SocialMedia t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(SocialMedia t) => await dal.UpdateAsync(t);
    public async Task<List<SocialMedia>> TGetListAsync() => await dal.GetListAsync();
    public async Task<SocialMedia?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
