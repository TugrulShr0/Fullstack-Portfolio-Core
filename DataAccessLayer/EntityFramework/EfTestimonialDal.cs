namespace DataAccessLayer.EntityFramework;

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

public class EfTestimonialDal : GenericRepository<Testimonial>, ITestimonialDal
{
    public EfTestimonialDal(Context context) : base(context) { }
    public EfTestimonialDal() : base() { }
}
