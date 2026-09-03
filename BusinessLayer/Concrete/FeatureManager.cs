namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FeatureManager(IFeatureDal dal) : IFeatureService
{
    public void TAdd(Feature t) => dal.Insert(t);
    public void TDelete(Feature t) => dal.Delete(t);
    public void TUpdate(Feature t) => dal.Update(t);
    public List<Feature> TGetList() => dal.GetList();
    public Feature? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Feature t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Feature t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Feature t) => await dal.UpdateAsync(t);
    public async Task<List<Feature>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Feature?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
