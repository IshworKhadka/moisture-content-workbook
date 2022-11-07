using MoistureContent.Models;
using System.Web.Mvc;

namespace MoistureContent.Web
{
    public interface IService
    {
        public JsonResult PartcleSizeDropdownFill();
        public JsonResult GetAllValues();

        public string Calculate(Result model);

        public double CalculateByMethod(Result model);
    }
}
