namespace MoistureContent.Models
{
    public class PreparationModel
    {
        public MethodType Method_Type { get; set; }

        public double Temperature { get; set; } = 110;

        public string Balance { get; set; }

        public int Particle_Size { get; set; }

        public bool IsMaterialExcluded { get; set; }

        public string Materials { get; set; }

        public string Oven { get; set; }
    }

    public enum MethodType
    {
        A, B
    }
}
