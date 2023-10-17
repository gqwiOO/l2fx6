using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace AndroiApp.Classes
{
    internal class Settings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        

        public static string DeviceName
        {
            get => DeviceInfo.Model;
            set => DeviceName = DeviceInfo.Model;
        }
        public static string IP
        {
            get => GetLocalAddress();
        }



        private static string GetLocalAddress()
        {
            var IpAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();
            if (IpAddress != null)
            {
                return IpAddress.ToString();
            }
            else
            {
                return "Could not locate IP Address";
            }
        }
    }

}
