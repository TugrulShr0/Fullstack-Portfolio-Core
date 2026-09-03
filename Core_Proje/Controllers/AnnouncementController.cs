using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementManager announcementManager = new AnnouncementManager(new EfAnnouncementDal());

        public IActionResult Index()
        {
            var values = announcementManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAnnouncement(Announcement p)
        {
            if (ModelState.IsValid)
            {
                p.Date = DateTime.Now;
                announcementManager.TAdd(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public IActionResult DeleteAnnouncement(int id)
        {
            var value = announcementManager.TGetByID(id);
            if (value != null)
            {
                announcementManager.TDelete(value);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditAnnouncement(int id)
        {
            var value = announcementManager.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAnnouncement(Announcement p)
        {
            if (ModelState.IsValid)
            {
                var current = announcementManager.TGetByID(p.ID);
                if (current != null)
                {
                    current.Title = p.Title;
                    current.Content = p.Content;
                    current.Status = p.Status;
                    announcementManager.TUpdate(current);
                }
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}
