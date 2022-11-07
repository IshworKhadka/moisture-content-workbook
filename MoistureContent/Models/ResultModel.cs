namespace MoistureContent.Models
{
    public class ResultModel
    {
        public double WaterContent { get; set; }

        public ResultType Report { get; set; }
    }


    public enum ResultType
    {
        Insufficient, DryingTemperature, MaterialExcluded
    }

    public class Result
    {
        public MethodType Method_Type { get; set; }
        public double TareMass { get; set; }

        public double TareAndMaterialWetMass { get; set; }

        public double TareAndMaterialDryMass { get; set; }

    }
}
