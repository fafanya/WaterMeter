using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using WaterMeter.Common.Models;
using WaterMeter.Models;
using WaterMeter.Views;

namespace WaterMeter.ViewModels
{
    public class MeasurementListViewModel : BaseViewModel
    {
        public ObservableCollection<TMeasurement> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddCommand { get; private set; }
        public INavigation Navigation { get; private set; }

        public MeasurementListViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Title = "Browse";
            Items = new ObservableCollection<TMeasurement>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddCommand = new Command(async () => await ExecuteAddCommand());

            MessagingCenter.Subscribe<NewMeasurementPage, MeasurementLocal>(this, "NewMeasurement", async (obj, fileItem) =>
            {
                await DataStore.AddPhotoAsync(fileItem);
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteAddCommand()
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewMeasurementPage()));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}