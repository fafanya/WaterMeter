using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaterMeter.MobileAppService.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Counter> Counters { get; set; }
    }
}