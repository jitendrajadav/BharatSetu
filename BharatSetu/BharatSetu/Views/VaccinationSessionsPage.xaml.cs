using BharatSetu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VaccinationSessionsPage : ContentPage
    {
        VaccinationSessionsViewModel _viewModel;
        public VaccinationSessionsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new VaccinationSessionsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}