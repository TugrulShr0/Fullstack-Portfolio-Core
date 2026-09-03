namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfPortfolioDal : GenericRepository<Portfolio>, IPortfolioDal
{
    public EfPortfolioDal(Context context) : base(context) { }
    public EfPortfolioDal() : base() { }
}
