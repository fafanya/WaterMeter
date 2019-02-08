using System;
using System.Collections.Generic;
using System.Text;
using WaterMeter.Models;
using Plugin.Media.Abstractions;

namespace WaterMeter.Models
{
    public class Counter
    {
        public Item Details { get; set; }
        public MediaFile Photo { get; set; }
    }
}
