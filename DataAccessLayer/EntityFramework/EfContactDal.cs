namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfContactDal : GenericRepository<Contact>, IContactDal
{
    public EfContactDal(Context context) : base(context) { }
    public EfContactDal() : base() { }
}
