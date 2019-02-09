namespace WaterMeter.Common.Models
{
    public class TMeasurement
    {
        public int TMeasurementId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string PhotoClientPath { get; set; }
        public string PhotoServerPath { get; set; }
    }
}