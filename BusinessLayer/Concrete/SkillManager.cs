namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SkillManager(ISkillDal dal) : ISkillService
{
    public void TAdd(Skill t) => dal.Insert(t);
    public void TDelete(Skill t) => dal.Delete(t);
    public void TUpdate(Skill t) => dal.Update(t);
    public List<Skill> TGetList() => dal.GetList();
    public Skill? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Skill t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Skill t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Skill t) => await dal.UpdateAsync(t);
    public async Task<List<Skill>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Skill?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
