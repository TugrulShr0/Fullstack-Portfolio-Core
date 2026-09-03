using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class ContactController : Controller
    {
        private readonly MessageManager messageManager = new MessageManager(new EfMessageDal());

        public IActionResult Index()
        {
            var values = messageManager.TGetList();
            return View(values);
        }

        public IActionResult DeleteContact(int id)
        {
            var values = messageManager.TGetByID(id);
            if (values != null)
            {
                messageManager.TDelete(values);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ContactDetails(int id)
        {
            var values = messageManager.TGetByID(id);
            if (values == null)
            {
                return RedirectToAction("Index"); 
            }

            // Mesaj detayı görüntülendiğinde Otomatik Okundu (Status = true) güncellemesi
            if (!values.Status)
            {
                values.Status = true;
                messageManager.TUpdate(values);
            }

            return View(values);
        }
    }
}
