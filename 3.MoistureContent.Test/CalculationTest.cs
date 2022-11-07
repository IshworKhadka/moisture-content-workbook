using MoistureContent.Controllers;
using MoistureContent.Models;
using MoistureContent.Web;
using Moq;

namespace MoistureContent.Test
{
    public class CalculationTest
    {

        #region Property  
        public Mock<IService> mock = new Mock<IService>();
        #endregion

        ///<summary>
        /// Unit Testing for Water Content Calucation
        /// </summary>
        [Fact]
        public void Calculate()
        {
            var model = new Result
            {
                Method_Type = MethodType.B,
                TareAndMaterialDryMass = 2525.7,
                TareAndMaterialWetMass = 2859.6,
                TareMass = 300.0
            };
            mock.Setup(p => p.Calculate(model)).Returns("15.0");
            HomeController controller = new HomeController();
            var result =  controller.Calculate(model);
            Assert.Equal("15.0", result);

        }
    }
}