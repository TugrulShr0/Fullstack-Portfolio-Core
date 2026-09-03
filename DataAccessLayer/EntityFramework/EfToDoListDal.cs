namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfToDoListDal : GenericRepository<ToDoList>, IToDoListDal
{
    public EfToDoListDal(Context context) : base(context) { }
    public EfToDoListDal() : base() { }
}
