using BharatSetu.Services;
using Plugin.LocalNotification;
using Xamarin.Forms;

namespace BharatSetu
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            // Local Notification received event listener
            NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;

            // Local Notification tap event listener
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;

            MainPage = new AppShell();
        }

        private void OnLocalNotificationReceived(NotificationReceivedEventArgs e)
        {
            // your code goes here
        }

        private void OnLocalNotificationTapped(NotificationTappedEventArgs e)
        {
            // your code goes here
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
