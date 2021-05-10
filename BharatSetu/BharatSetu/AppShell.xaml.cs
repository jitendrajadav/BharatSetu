using BharatSetu.Views;
using System;
using Xamarin.Forms;

namespace BharatSetu
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(CalendarByDistrictPage), typeof(CalendarByDistrictPage));
            Routing.RegisterRoute(nameof(CalendarByPinPage), typeof(CalendarByPinPage));
            Routing.RegisterRoute(nameof(FindByDistrictPage), typeof(FindByDistrictPage));
            Routing.RegisterRoute(nameof(FindByPinPage), typeof(FindByPinPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(StatesPage), typeof(StatesPage));
            Routing.RegisterRoute(nameof(DistrictsPage), typeof(DistrictsPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
