using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class connectToDevicePage : ContentPage
    {
        public connectToDevicePage()
        {
            InitializeComponent();


        }

        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}