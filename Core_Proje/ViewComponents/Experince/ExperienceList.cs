using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.ViewComponents.Experince
{
    public class ExperienceList : ViewComponent
    {
        ExperienceManager experienceManager = new ExperienceManager(new DataAccessLayer.EntityFramework.EfExperienceDal());
        public IViewComponentResult Invoke()
        {
            var values = experienceManager.TGetList();

            return View(values);
        }
    }
}
