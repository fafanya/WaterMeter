using WaterMeter.Common.Models;

namespace WaterMeter.ViewModels
{
    public class MeasurementDetailViewModel : BaseViewModel
    {
        public TMeasurement Item { get; set; }
        public MeasurementDetailViewModel(TMeasurement item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}