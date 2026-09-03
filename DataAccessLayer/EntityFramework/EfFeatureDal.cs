namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfFeatureDal : GenericRepository<Feature>, IFeatureDal
{
    public EfFeatureDal(Context context) : base(context) { }
    public EfFeatureDal() : base() { }
}
