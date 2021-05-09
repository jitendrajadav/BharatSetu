using BharatSetu.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatesPage : ContentPage
    {
        StatesViewModel _viewModel;
        public StatesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StatesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}