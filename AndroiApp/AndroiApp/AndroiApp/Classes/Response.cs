using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AndroiApp.Classes
{
    public class ResponseViewModel : INotifyPropertyChanged
    {
        private DateTime time;
        private string ip_sender;
        private string response;
        private string ip_receiver;

        public DateTime Time
        {
            get { return time; }
            private set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public string IP_Sender
        {
            get { return ip_sender; }
            private set
            {
                ip_sender = value;
                OnPropertyChanged("IP_Sender");
            }
        }

        public string IP_Receiver
        {
            get { return ip_receiver; }
            private set
            {
                ip_sender = value;
                OnPropertyChanged("IP_Receiver");
            }
        }
        public string Response
        {
            get { return response; }
            private set
            {
                response = value;
                OnPropertyChanged("Response");
            }
        }

        public ICommand LoadDataCommand { protected set; get; }

        public ResponseViewModel()
        {
            this.LoadDataCommand = new Command(LoadData);
        }

        private async void LoadData()
        {
            var response = Settings.Response;

            JObject jsonResponse= JObject.Parse(response);

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
