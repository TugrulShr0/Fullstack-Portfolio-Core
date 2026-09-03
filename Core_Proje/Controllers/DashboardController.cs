using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Index()
        {
   
            return View();
        }
    }
}
