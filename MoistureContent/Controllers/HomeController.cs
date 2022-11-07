using Microsoft.AspNetCore.Mvc;
using MoistureContent.Models;
using MoistureContent.Web;
using System.Diagnostics;

namespace MoistureContent.Controllers
{
    public class HomeController : Controller, IService
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Authored Action Methods
        public JsonResult PartcleSizeDropdownFill()
        {
            var list = new List<string>();
            list.Add("3\" (75 mm)");
            list.Add("4\" (100 mm)");
            list.Add("6\" (150 mm)");

            return Json(list);
        }

        public JsonResult GetAllValues()
        {
            var obj = new WaterContentModel();
            return Json(obj);
        }

        public string Calculate(Result model)
        {
            //Validation performed
            if (model.TareMass <= 0)
            {
                return "A null, negative or 0 Tare value found";
            }

            if (model.TareAndMaterialWetMass < 0 || model.TareAndMaterialDryMass < 0)
            {
                return "Mass cannot be less than 0";
            }

            if (model.TareAndMaterialDryMass > model.TareAndMaterialWetMass)
            {
                return "Dry mass is greater than wet mass";
            }

            if (model.TareMass >= model.TareAndMaterialWetMass || model.TareMass >= model.TareAndMaterialDryMass)
            {
                return "Tare mass cannot be greater";
            }

            var result = CalculateByMethod(model);
            if (model.Method_Type == MethodType.B)
            {
                return result.ToString("0.0");
            }
            else
            {
                return result.ToString("0");
            }
        }

        public double CalculateByMethod(Result model)
        {
            var result = (model.TareAndMaterialWetMass - model.TareAndMaterialDryMass) / (model.TareAndMaterialDryMass - model.TareMass);
            return (result * 100);
        }
        #endregion

        #region Default ErrorHandlers
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        System.Web.Mvc.JsonResult IService.PartcleSizeDropdownFill()
        {
            throw new NotImplementedException();
        }

        System.Web.Mvc.JsonResult IService.GetAllValues()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}