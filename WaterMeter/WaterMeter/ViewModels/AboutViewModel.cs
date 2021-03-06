﻿using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace WaterMeter.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://sys-prog.com/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}