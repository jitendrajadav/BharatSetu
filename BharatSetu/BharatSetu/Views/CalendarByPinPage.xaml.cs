using BharatSetu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarByPinPage : ContentPage
    {
        CalendarByPinViewModel _viewModel;
        public CalendarByPinPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CalendarByPinViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}