namespace MoistureContent.Models
{
    public class WaterContentModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string ProjectName { get; set; } = "Moisture Content Calculator";
        public string SampledDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

        public string SourceAndMaterial { get; set; }

        public string Specification { get; set; }

        public PreparationModel Preparationmodels { get; set; }

        public MeasurementModel MeasurementModels { get; set; } 

        public DryMassModel DryMassModels { get; set; }

        public string TestedBy { get; set; } = "Ishwor";

        public string TestedDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

        public string CheckedBy { get; set; } = "Ishwor";

        public string DateChecked { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    }   
}
