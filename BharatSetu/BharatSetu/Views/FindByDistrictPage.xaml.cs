using Xamarin.Forms;

namespace BharatSetu.Views
{
    public partial class FindByDistrictPage : ContentPage
    {
        public FindByDistrictPage()
        {
            InitializeComponent();
        }

        private void ItemsListView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {

            double scrollY = e.VerticalOffset < 0 ? 0 : e.VerticalOffset;
            scrollY = scrollY > ItemsListView.Height ? ItemsListView.Height : scrollY;
           
            if (scrollY >= 50 && Shell.GetNavBarIsVisible(self) is true)
            {
                Shell.SetNavBarIsVisible(self, false);
                Shell.SetFlyoutBehavior(self, FlyoutBehavior.Disabled);
            }
            if (scrollY == 0 && Shell.GetNavBarIsVisible(self) is false)
            {
                Shell.SetNavBarIsVisible(self, true);
                Shell.SetFlyoutBehavior(self, FlyoutBehavior.Flyout);
            }
        }
    }
}