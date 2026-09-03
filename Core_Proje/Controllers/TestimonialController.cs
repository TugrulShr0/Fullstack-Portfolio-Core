using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Core_Proje.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class TestimonialController : Controller
    {
        private readonly TestimonialManager testimonialManager = new TestimonialManager(new EfTestimonialDal());

        public IActionResult Index()
        {
            var values = testimonialManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTestimonial(Testimonial p, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                p.ImageUrl = await SaveImageAsync(ImageFile);
            }
            else
            {
                p.ImageUrl = "/userimage/default-avatar.png";
            }

            testimonialManager.TAdd(p);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTestimonial(int id)
        {
            var values = testimonialManager.TGetByID(id);
            if (values != null)
            {
                FileHelper.DeleteFile(values.ImageUrl);
                testimonialManager.TDelete(values);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTestimonial(int id)
        {
            var values = testimonialManager.TGetByID(id);
            if (values == null)
            {
                return RedirectToAction("Index");
            }
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTestimonial(Testimonial testimonial, IFormFile? ImageFile)
        {
            var current = testimonialManager.TGetByID(testimonial.TestimonialID);
            if (current != null)
            {
                current.ClientName = testimonial.ClientName;
                current.Company = testimonial.Company;
                current.Title = testimonial.Title;
                current.Comment = testimonial.Comment;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    FileHelper.DeleteFile(current.ImageUrl);
                    current.ImageUrl = await SaveImageAsync(ImageFile);
                }

                testimonialManager.TUpdate(current);
            }

            return RedirectToAction("Index");
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var imageName = Guid.NewGuid() + extension;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, imageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/userimage/" + imageName;
        }
    }
}