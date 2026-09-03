namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfAboutDal : GenericRepository<About>, IAboutDal
{
    public EfAboutDal(Context context) : base(context) { }
    public EfAboutDal() : base() { }
}
