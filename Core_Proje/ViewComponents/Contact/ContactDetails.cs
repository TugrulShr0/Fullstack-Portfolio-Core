using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.ViewComponents.Contact
{
    public class ContactDetails: ViewComponent
    {
        ContactManager contactManager = new ContactManager(new DataAccessLayer.EntityFramework.EfContactDal());
        public IViewComponentResult Invoke()
        {
           var values = contactManager.TGetList();
            return View(values);
        }
     
    }
}
