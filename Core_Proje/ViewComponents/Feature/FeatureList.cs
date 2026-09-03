
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.ViewCompanents.Feature
  
{
    public class FeatureList : ViewComponent
    {
        FeatureManager featureManager = new FeatureManager(new DataAccessLayer.EntityFramework.EfFeatureDal());
        public IViewComponentResult Invoke()
        {
            var values = featureManager.TGetList();
            return View(values);
        }


    }

 
}
