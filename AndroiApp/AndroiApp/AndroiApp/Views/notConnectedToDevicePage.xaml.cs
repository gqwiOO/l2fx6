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
using System.IO;

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
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.sqlite3")))
            {
                try
                {
                    Dictionary<string, string> functions = Server.GetDictionaryFunctionsFromDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.sqlite3"));
                    //DisplayAlert("TITLE", $"{functions["play dota"]}", "yes", "no");
                }
                catch (Exception ex)
                {
                    DisplayAlert("title", ex.Message, "yes", "no");
                }

            }
            else if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "response.json")))
            {
                DisplayAlert("TITLE", "response exists", "yes", "no");

            }

        }

        private void sendMessage(object sender, EventArgs e)
        {
            //DisplayAlert("Title", Settings.Response, "ok");
            
            Server server = new Server();

            server.sendResponse("fhadsiufhdsfjsdkfhsdf", "192.168.0.101");
            server.receiveResponse();
            
            

        }
    }
}