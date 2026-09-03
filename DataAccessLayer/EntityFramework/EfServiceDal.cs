namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfServiceDal : GenericRepository<Service>, IServiceDal
{
    public EfServiceDal(Context context) : base(context) { }
    public EfServiceDal() : base() { }
}
