using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaterMeter.MobileAppService.Models
{
    public class Counter
    {
        public int CounterId { get; set; }
        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Measurement> Measurements { get; set; }
    }
}