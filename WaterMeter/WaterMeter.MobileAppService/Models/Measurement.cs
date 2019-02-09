using System;
using System.ComponentModel.DataAnnotations;

namespace WaterMeter.MobileAppService.Models
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Stamp { get; set; }

        [Required]
        public string PhotoClientPath { get; set; }
        [Required]
        public string PhotoServerPath { get; set; }

        public int CounterId { get; set; }
        public Counter Counter { get; set; }
    }
}