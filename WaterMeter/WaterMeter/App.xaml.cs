using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WaterMeter.Services;
using WaterMeter.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WaterMeter
{
    public partial class App : Application
    {
        public static string BackendUrl = "http://192.168.100.4:5000";
        //public static string BackendUrl = "http://192.168.0.73:5000";

        public App()
        {
            InitializeComponent();

            DependencyService.Register<BackendDataStore>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}