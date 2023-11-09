using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using AndroiApp.Classes;

namespace AndroiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class notConnectedToDevicePage : ContentPage
    {
        public notConnectedToDevicePage()
        {
            InitializeComponent();
        }

        private async void connectToDeviceButton(object sender, EventArgs e)
        {
            var wifi_status = Connectivity.NetworkAccess;

            if (wifi_status != NetworkAccess.Internet)
            {
                await Navigation.PushModalAsync(new errorNotConnectedToWifi());
            }
            else
            {
                await Navigation.PushModalAsync(new connectToDevicePage());

            }
        }
        private async void settingsButton(object sendert, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
            
            
        }

        private void sendMessage(object sender, EventArgs e)
        {
            //DisplayAlert("Title", Settings.Response, "ok");
            
            Server server = new Server();

            server.sendResponse("fhadsiufhdsfjsdkfhsdf", "192.168.0.101");

        }
    }
}