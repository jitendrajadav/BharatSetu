using BharatSetu.Services;
using BharatSetu.Views;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BharatSetu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            if (Device.UWP != Device.RuntimePlatform)
            {
                // Local Notification tap event listener
                NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped; 
            }

            MainPage = new AppShell();
        }

        private void OnLocalNotificationReceived(NotificationReceivedEventArgs e)
        {
            // your code goes here
        }

        private  void OnLocalNotificationTapped(NotificationTappedEventArgs e)
        {
            // your code goes here
            if (string.IsNullOrWhiteSpace(e.Request.ReturningData))
            {
                return;
            }

            var list = ObjectSerializer.DeserializeObject<List<string>>(e.Request.ReturningData);
            if (list.Count != 2)
            {
                return;
            }
            if (list[0] != typeof(NotificationPage).FullName)
            {
                return;
            }
            var tapCount = list[1];

            ((NavigationPage)MainPage).Navigation.PushAsync(new NotificationPage(int.Parse(tapCount)));
        }

        protected override void OnStart()
        {
            // Token event
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };
            //Push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
