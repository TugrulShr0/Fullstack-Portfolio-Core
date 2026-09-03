using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class SocialMediaController : Controller
    {
        private readonly SocialMediaManager socialMediaManager = new SocialMediaManager(new EfSocialMediaDal());
        
        public IActionResult Index()
        {
            var values = socialMediaManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSocialMedia(SocialMedia p)
        {
            p.Status = true;
            socialMediaManager.TAdd(p);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSocialMedia(int id)
        {
            var values = socialMediaManager.TGetByID(id);
            if (values != null)
            {
                socialMediaManager.TDelete(values);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSocialMedia(int id)
        {
            var values = socialMediaManager.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSocialMedia(SocialMedia p)
        {
            socialMediaManager.TUpdate(p);
            return RedirectToAction("Index");
        }
    }
}
