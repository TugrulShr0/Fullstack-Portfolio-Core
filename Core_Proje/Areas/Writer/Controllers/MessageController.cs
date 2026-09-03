using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Admin,Writer,Moderator")]
    [Route("Writer/Message")]
    public class MessageController : Controller
    {
        private readonly WriterMessageManager writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
        private readonly UserManager<WriterUser> _userManager;

        public MessageController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        [Route("ReceiverMessage")]
        public async Task<IActionResult> ReceiverMessage(string p)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (values == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            p = values.Email;
            var messageList = writerMessageManager.GetListReceiverMessage(p);
            return View(messageList);
        }

        [HttpGet]
        [Route("SenderMessage")]
        public async Task<IActionResult> SenderMessage(string p)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (values == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            p = values.Email;
            var messageList = writerMessageManager.GetListSenderMessage(p);
            return View(messageList);
        }

        [HttpGet]
        [Route("MessageDetails/{id}")]
        public IActionResult MessageDetails(int id)
        {
            WriterMessage writerMessage = writerMessageManager.TGetByID(id);
            return View(writerMessage);
        }

        [HttpGet]
        [Route("ReceiverMessageDetails/{id}")]
        public IActionResult ReceiverMessageDetails(int id)
        {
            WriterMessage writerMessage = writerMessageManager.TGetByID(id);
            return View(writerMessage);
        }

        [HttpGet]
        [Route("SendMessage")]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(WriterMessage p)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (values == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Writer" });
            }

            p.Sender = values.Email ?? "Bilinmeyen";
            p.SenderName = $"{values.Name} {values.Surname}".Trim();
            if (string.IsNullOrWhiteSpace(p.SenderName))
            {
                p.SenderName = values.UserName;
            }

            p.Date = DateTime.Now;
            p.Subject = string.IsNullOrWhiteSpace(p.Subject) ? "Konusuz" : p.Subject;
            p.MessageContent = string.IsNullOrWhiteSpace(p.MessageContent) ? "İçerik yok" : p.MessageContent;

            using (Context c = new Context())
            {
                var receiverUser = c.Users.FirstOrDefault(x => x.Email == p.Receiver);

                if (receiverUser != null)
                {
                    string fullName = $"{receiverUser.Name} {receiverUser.Surname}".Trim();
                    p.ReceiverName = string.IsNullOrWhiteSpace(fullName) ? receiverUser.UserName : fullName;
                }
                else
                {
                    p.ReceiverName = !string.IsNullOrWhiteSpace(p.Receiver) ? p.Receiver : "Bilinmeyen Alıcı";
                }
            }

            writerMessageManager.TAdd(p);
            return RedirectToAction("SenderMessage");
        }
    }
}