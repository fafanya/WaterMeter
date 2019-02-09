using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WaterMeter.Models;
using WaterMeter.ViewModels;
using WaterMeter.Common.Models;

namespace WaterMeter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeasurementDetailPage : ContentPage
    {
        MeasurementDetailViewModel viewModel;

        public MeasurementDetailPage(MeasurementDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            image.Source = ImageSource.FromFile(viewModel.Item.PhotoClientPath);
        }

        public MeasurementDetailPage()
        {
            InitializeComponent();

            var item = new TMeasurement
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new MeasurementDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}