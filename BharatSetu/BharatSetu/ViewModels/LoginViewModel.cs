using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
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
                otp = ComputeSha256Hash(OTP),
                txnId = TxnId
            };
            var confirmMsg = await DataStore.ConfirmOTP(confirm);
            if (confirmMsg.IsSuccessStatusCode)
            {
                var response = await confirmMsg.Content.ReadAsStringAsync();
                var items = await Task.Run(() => JsonConvert.DeserializeObject<AuthConfirm>(response, GetJsonSetting()));
                Xamarin.Essentials.Preferences.Set("token", items.token);
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(StatesPage)}");
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
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
