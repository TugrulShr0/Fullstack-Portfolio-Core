using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactSubplaceController : Controller
    {
        private readonly ContactManager contactManager = new ContactManager(new EfContactDal());

        [HttpGet]
        public IActionResult Index()
        {
            var values = contactManager.TGetByID(1);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contactManager.TUpdate(contact);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(contact);
        }
    }
}
