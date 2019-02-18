using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using WaterMeter.Common.Models;
using WaterMeter.Models;

namespace WaterMeter.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewMeasurementPage : ContentPage
	{
        public MeasurementLocal Measurement { get; set; }

        public NewMeasurementPage()
        {
            InitializeComponent();

            Measurement = new MeasurementLocal { 
                Details = new TMeasurement ()
            };

            BindingContext = this;
        }

        async void TakePhoto_Clicked(object sender, System.EventArgs e)
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                if (cameraStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Camera permission", "Camera permission", "OK");
                    }
                }

                if (storageStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Storage permission", "Storage permission", "OK");
                    }
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                //OverlayViewProvider = func,
                Directory = "Sample",
                Name = "test.jpg"
            });

            MemoryStream ms = new MemoryStream();
            file.GetStream().CopyTo(ms);
            Measurement.Photo = ms.ToArray();
            Measurement.PhotoPath = file.Path;
            Measurement.Details.PhotoClientPath = file.Path;

            image.Source = ImageSource.FromStream(()=> 
            {
                return file.GetStream();
            } );
        }

        Func<object> func = () =>
        {
            var layout = new RelativeLayout();
            var image = new Image
            {
                Source = "counter"
            };

            double ImageHeight(RelativeLayout p) => image.Measure(layout.Width, layout.Height).Request.Height;
            double ImageWidth(RelativeLayout p) => image.Measure(layout.Width, layout.Height).Request.Width;

            layout.Children.Add(image,
                        Constraint.RelativeToParent(parent => parent.Width / 2 - ImageWidth(parent) / 2),
                        Constraint.RelativeToParent(parent => parent.Height / 2 - ImageHeight(parent) / 2)
            );
            return layout;
        };

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Measurement.Photo != null)
            {
                MessagingCenter.Send(this, "NewMeasurement", Measurement);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("New Measurement", "Photo can not be empty", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}