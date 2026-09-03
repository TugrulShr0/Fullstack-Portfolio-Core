using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Admin,Writer,Moderator")]
    public class DashboardController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public DashboardController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (values == null) return RedirectToAction("Index", "Login", new { area = "Writer" });

            ViewBag.v = (values.Name + " " + values.Surname).Trim();
            if (string.IsNullOrWhiteSpace(ViewBag.v))
            {
                ViewBag.v = values.UserName;
            }

            // Weather API (Ankara)
            string api = "14ad2aba611dbef9c504b82a127794c5";
            string connection = $"https://api.openweathermap.org/data/2.5/weather?q=ankara&mode=xml&lang=tr&units=metric&appid={api}";

            try
            {
                XDocument document = XDocument.Load(connection);
                ViewBag.v5 = document.Descendants("temperature").ElementAt(0).Attribute("value")?.Value;
            }
            catch (Exception)
            {
                ViewBag.v5 = "--";
            }

            // Statistics
            string userEmail = values.Email ?? "";
            using (Context c = new Context())
            {
                ViewBag.v1 = c.WriterMessages.Count(x => x.Receiver == userEmail);
                ViewBag.v2 = c.Announcements.Count();
                ViewBag.v3 = c.Users.Count();
                ViewBag.v4 = c.WriterMessages.Count(x => x.Sender == userEmail);
            }

            return View();
        }
    }
}