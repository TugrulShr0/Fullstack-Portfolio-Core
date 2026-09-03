namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfExperienceDal : GenericRepository<Experience>, IExperienceDal
{
    public EfExperienceDal(Context context) : base(context) { }
    public EfExperienceDal() : base() { }
}
