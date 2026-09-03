namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TestimonialManager(ITestimonialDal dal) : ITestimonialService
{
    public void TAdd(Testimonial t) => dal.Insert(t);
    public void TDelete(Testimonial t) => dal.Delete(t);
    public void TUpdate(Testimonial t) => dal.Update(t);
    public List<Testimonial> TGetList() => dal.GetList();
    public Testimonial? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Testimonial t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Testimonial t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Testimonial t) => await dal.UpdateAsync(t);
    public async Task<List<Testimonial>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Testimonial?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
