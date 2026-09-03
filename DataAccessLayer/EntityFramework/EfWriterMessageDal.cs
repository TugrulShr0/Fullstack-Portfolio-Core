namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfWriterMessageDal : GenericRepository<WriterMessage>, IWriterMessageDal
{
    public EfWriterMessageDal(Context context) : base(context) { }
    public EfWriterMessageDal() : base() { }
}
