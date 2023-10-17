using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AndroiApp.Classes;
using Xamarin.Essentials;

namespace AndroiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            phoneModel.Text = DeviceInfo.Name;
            IP.Text = Settings.IP;
           
        }

        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}