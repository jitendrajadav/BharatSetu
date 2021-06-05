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
    }
}