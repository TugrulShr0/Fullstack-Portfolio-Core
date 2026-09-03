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
    [Authorize(Roles = "Admin")]
    public class FeatureController : Controller
    {
        private readonly FeatureManager featureManager = new FeatureManager(new EfFeatureDal());

        [HttpGet]
        public IActionResult Index()
        {
            var values = featureManager.TGetByID(1);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Feature feature, IFormFile Picture)
        {
            var currentFeature = featureManager.TGetByID(feature.FeatureID);

            if (currentFeature != null)
            {
                currentFeature.Header = feature.Header;
                currentFeature.Name = feature.Name;
                currentFeature.Title = feature.Title;

                if (Picture != null && Picture.Length > 0)
                {
                    FileHelper.DeleteFile(currentFeature.ImageUrl);

                    var extension = Path.GetExtension(Picture.FileName);
                    var newImageName = Guid.NewGuid() + extension;

                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var saveLocation = Path.Combine(folderPath, newImageName);
                    using (var stream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await Picture.CopyToAsync(stream);
                    }

                    currentFeature.ImageUrl = "/userimage/" + newImageName;
                }

                featureManager.TUpdate(currentFeature);
            }

            return RedirectToAction("Index");
        }
    }
}