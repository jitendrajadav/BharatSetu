using BharatSetu.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BharatSetu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatePage : ContentPage
    {
        StateViewModel _viewModel;
        public StatePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StateViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}