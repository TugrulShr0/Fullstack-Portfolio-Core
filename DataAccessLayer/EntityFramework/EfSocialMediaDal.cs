namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfSocialMediaDal : GenericRepository<SocialMedia>, ISocialMediaDal
{
    public EfSocialMediaDal(Context context) : base(context) { }
    public EfSocialMediaDal() : base() { }
}
