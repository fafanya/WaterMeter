using WaterMeter.Common.Models;

namespace WaterMeter.Models
{
    public class MeasurementLocal
    {
        public TMeasurement Details { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoPath { get; set; }
    }
}