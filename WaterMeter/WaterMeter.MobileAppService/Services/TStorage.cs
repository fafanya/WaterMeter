using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterMeter.MobileAppService.Services
{
    public class TStorage
    {
        public int ID { get; set; }
        public string Table { get; set; }
        public string PKField { get; set; }
        public Dictionary<string, object> Fields { get; set; }

        public TStorage()
        {
            Fields = new Dictionary<string, object>();
        }
    }
}