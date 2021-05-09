using BharatSetu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindByPinPage : ContentPage
    {
        FindByPinViewModel _viewModel;
        public FindByPinPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FindByPinViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}