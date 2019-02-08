using System;
using System.Collections.Generic;
using System.Text;
using WaterMeter.Models;
using Xamarin.Forms;
using WaterMeter.Views;
using Plugin.Media.Abstractions;

namespace WaterMeter.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        public PhotoViewModel()
        {
            MessagingCenter.Subscribe<PhotoPage, Counter>(this, "AddPhoto", async (obj, fileItem) =>
            {
                await DataStore.AddPhotoAsync(fileItem);
            });
        }
    }
}
