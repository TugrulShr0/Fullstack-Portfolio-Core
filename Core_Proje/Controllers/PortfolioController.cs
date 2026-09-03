using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")] 
    public class PortfolioController : Controller
    {
        private readonly PortfolioManager portfolioManager = new PortfolioManager(new EfPortfolioDal());

        public IActionResult Index()
        {
            var values = portfolioManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddPortfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolio(EntityLayer.Concrete.Portfolio p, IFormFile? ImageFile1, IFormFile? ImageFile2, IFormFile? PlatformFile)
        {
            if (ImageFile1 != null && ImageFile1.Length > 0)
            {
                p.ImageUrl = await SaveImageAsync(ImageFile1);
            }

            if (ImageFile2 != null && ImageFile2.Length > 0)
            {
                p.ImageUrl2 = await SaveImageAsync(ImageFile2);
            }

            if (PlatformFile != null && PlatformFile.Length > 0)
            {
                p.Platform = await SaveImageAsync(PlatformFile);
            }

            PortfolioValidator validations = new PortfolioValidator();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                portfolioManager.TAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            ViewBag.v1 = "Proje Listesi";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Ekleme";
            return View(p);
        }

        public IActionResult DeletePortfolio(int id)
        {
            var value = portfolioManager.TGetByID(id);
            if (value != null)
            {
                portfolioManager.TDelete(value);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPortfolio(int id)
        {
            ViewBag.v1 = "Proje Listesi";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Güncelleme";
            var value = portfolioManager.TGetByID(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> EditPortfolio(EntityLayer.Concrete.Portfolio p, IFormFile? ImageFile1, IFormFile? ImageFile2, IFormFile? PlatformFile)
        {
            if (ImageFile1 != null && ImageFile1.Length > 0)
            {
                p.ImageUrl = await SaveImageAsync(ImageFile1);
            }

            if (ImageFile2 != null && ImageFile2.Length > 0)
            {
                p.ImageUrl2 = await SaveImageAsync(ImageFile2);
            }

            if (PlatformFile != null && PlatformFile.Length > 0)
            {
                p.Platform = await SaveImageAsync(PlatformFile);
            }

            PortfolioValidator validations = new PortfolioValidator();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                portfolioManager.TUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            ViewBag.v1 = "Proje Listesi";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Güncelleme";
            return View(p);
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