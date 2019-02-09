using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WaterMeter.Common.Models;
using WaterMeter.ViewModels;

namespace WaterMeter.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeasurementListPage : ContentPage
	{
        MeasurementListViewModel viewModel;

        public MeasurementListPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MeasurementListViewModel(Navigation);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is TMeasurement item))
                return;

            await Navigation.PushAsync(new MeasurementDetailPage(new MeasurementDetailViewModel(item)));
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }

        async void ImageButton_ItemTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewMeasurementPage()));
        }
    }
}