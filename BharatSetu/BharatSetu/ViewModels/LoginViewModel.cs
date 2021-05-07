using BharatSetu.Models;
using BharatSetu.Views;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Mobile { get; set; }
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            Mobile mobile =  new Mobile()
            { mobile = Mobile };
            var auth = await DataStore.BeneficiaryAuthentication(mobile);
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
