using BharatSetu.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistrictsPage : ContentPage
    {
        DistrictsViewModel _viewModel;
        public DistrictsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new DistrictsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}