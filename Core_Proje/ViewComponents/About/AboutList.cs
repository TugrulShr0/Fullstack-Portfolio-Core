using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Core_Proje.ViewComponents.About
{
    public class AboutList: ViewComponent
    {
        AboutManager aboutManager = new AboutManager(new DataAccessLayer.EntityFramework.EfAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = aboutManager.TGetList();
            return View(values);
        }
    }
}
