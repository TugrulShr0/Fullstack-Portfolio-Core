using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminMessageController : Controller
    {
        private readonly WriterMessageManager writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
        private readonly string adminEmail = "sahartu6@gmail.com";

        public IActionResult ReceiverMessageList()
        {
            var values = writerMessageManager.GetListReceiverMessage(adminEmail);
            return View(values);
        }

        public IActionResult SenderMessageList()
        {
            var values = writerMessageManager.GetListSenderMessage(adminEmail);
            return View(values);
        }

        public IActionResult AdminMessageDetails(int id)
        {
            var values = writerMessageManager.TGetByID(id);
            if (values == null)
            {
                return RedirectToAction("ReceiverMessageList");
            }
            return View(values);
        }

        public IActionResult AdminMessageDelete(int id, string returnType = "Receiver")
        {
            var values = writerMessageManager.TGetByID(id);
            if (values != null)
            {
                writerMessageManager.TDelete(values);
            }

            if (returnType == "Sender")
            {
                return RedirectToAction("SenderMessageList");
            }
            return RedirectToAction("ReceiverMessageList");
        }

        [HttpGet]
        public IActionResult AdminMessageSend()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminMessageSend(WriterMessage p)
        {
            p.Sender = adminEmail;
            p.SenderName = "Tuğrul Şahar";
            p.Date = DateTime.Now;

            using (var c = new Context())
            {
                var receiverUser = c.Users.FirstOrDefault(x => x.Email == p.Receiver);
                p.ReceiverName = receiverUser != null ? $"{receiverUser.Name} {receiverUser.Surname}" : (p.ReceiverName ?? "Kullanıcı");
            }

            writerMessageManager.TAdd(p);
            return RedirectToAction("SenderMessageList");
        }
    }
}