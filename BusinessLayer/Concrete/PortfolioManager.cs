namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PortfolioManager(IPortfolioDal dal) : IPortfolioService
{
    public void TAdd(Portfolio t) => dal.Insert(t);
    public void TDelete(Portfolio t) => dal.Delete(t);
    public void TUpdate(Portfolio t) => dal.Update(t);
    public List<Portfolio> TGetList() => dal.GetList();
    public Portfolio? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Portfolio t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Portfolio t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Portfolio t) => await dal.UpdateAsync(t);
    public async Task<List<Portfolio>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Portfolio?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
