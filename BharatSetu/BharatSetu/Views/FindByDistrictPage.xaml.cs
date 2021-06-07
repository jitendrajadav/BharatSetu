using BharatSetu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindByDistrictPage : ContentPage
    {
        private readonly FindByDistrictViewModel _viewModel;
        public FindByDistrictPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FindByDistrictViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
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