namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfAnnouncementDal : GenericRepository<Announcement>, IAnnouncementDal
{
    public EfAnnouncementDal(Context context) : base(context) { }
    public EfAnnouncementDal() : base() { }
}
