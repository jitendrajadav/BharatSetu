using BharatSetu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarByDistrictPage : ContentPage
    {
        CalendarByDistrictViewModel _viewModel;
        public CalendarByDistrictPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CalendarByDistrictViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}