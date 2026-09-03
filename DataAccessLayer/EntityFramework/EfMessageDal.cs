namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfMessageDal : GenericRepository<Message>, IMessageDal
{
    public EfMessageDal(Context context) : base(context) { }
    public EfMessageDal() : base() { }
}
