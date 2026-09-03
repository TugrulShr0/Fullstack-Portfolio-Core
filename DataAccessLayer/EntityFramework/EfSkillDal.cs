namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfSkillDal : GenericRepository<Skill>, ISkillDal
{
    public EfSkillDal(Context context) : base(context) { }
    public EfSkillDal() : base() { }
}
