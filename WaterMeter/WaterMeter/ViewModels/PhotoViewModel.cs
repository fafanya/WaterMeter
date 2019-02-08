using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using WaterMeter.Views;
using Plugin.Media.Abstractions;

namespace WaterMeter.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        public PhotoViewModel()
        {
            MessagingCenter.Subscribe<PhotoPage, MediaFile>(this, "AddPhoto", async (obj, photo) =>
            {
                await DataStore.AddPhotoAsync(photo);
            });
        }
    }
}
