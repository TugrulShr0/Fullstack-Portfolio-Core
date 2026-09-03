namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfWriterUserDal : GenericRepository<WriterUser>, IWriterUserDal
{
    public EfWriterUserDal(Context context) : base(context) { }
    public EfWriterUserDal() : base() { }
}
