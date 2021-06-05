using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties

        public string Mobile { get; set; }
        public string OTP { get; set; }
        public string TxnId { get; set; }

        public bool isOTPVisible = true;
        public bool IsOTPVisible
        {
            get => isOTPVisible;
            set => SetProperty(ref isOTPVisible, value);
        }

        private bool isConfirmVisible;
        public bool IsConfirmVisible
        {
            get => isConfirmVisible;
            set => SetProperty(ref isConfirmVisible, value);
        }

        #endregion

        #region Commands

        public Command LoginCommand => new Command(OnLoginClicked);
        public Command ConfirmCommand => new Command(OnConfirmClicked);

        #endregion

        #region Methods

        private async void OnConfirmClicked(object obj)
        {
            ConfirmAuthentication confirm = new ConfirmAuthentication()
            {
                otp = OTP,
                txnId = TxnId
            };
            var confirmMsg = await DataStore.ConfirmOTP(confirm);
            if (confirmMsg.IsSuccessStatusCode)
            {
                var response = await confirmMsg.Content.ReadAsStringAsync();
                var items = await Task.Run(() => JsonConvert.DeserializeObject<AuthConfirm>(response, GetJsonSetting()));
            }
            var response1 = await confirmMsg.Content.ReadAsStringAsync();

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(StatesPage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            Mobile mobile = new Mobile()
            { mobile = Mobile };
            var auth = await DataStore.GenerateOTP(mobile);
            if (auth.IsSuccessStatusCode)
            {
                var response = await auth.Content.ReadAsStringAsync();
                var items = await Task.Run(() => JsonConvert.DeserializeObject<Authenticate>(response, GetJsonSetting()));
                TxnId = items.txnId;
                IsOTPVisible = false;
                IsConfirmVisible = true;
            }
            else
            {

                IsOTPVisible = true;
                IsConfirmVisible = false;
            }

        }

        #endregion
    }
}
