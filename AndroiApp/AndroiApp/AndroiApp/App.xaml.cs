using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AndroiApp.Views;
using AndroiApp.Classes;

namespace AndroiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if(Settings.FirstRun)
            {
                MainPage = new WelcomePage();
                Settings.FirstRun = false;
            }
            else
            {
                MainPage = new notConnectedToDevicePage();

            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
