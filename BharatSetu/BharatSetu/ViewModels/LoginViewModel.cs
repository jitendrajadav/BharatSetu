﻿using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Mobile { get; set; }
        public string OTP { get; set; }
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
        public Command LoginCommand { get; }
        public Command ConfirmCommand { get;  }
        public string TxnId { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            ConfirmCommand = new Command(OnConfirmClicked);
        }

        private async void OnConfirmClicked(object obj)
        {
            ConfirmAuthentication confirm = new ConfirmAuthentication()
            {
                otp = OTP,
                txnId = TxnId
            };
            var confirmMsg = await DataStore.ConfirmAuthentication(confirm);
            if (confirmMsg.IsSuccessStatusCode)
            {
                var response = await confirmMsg.Content.ReadAsStringAsync();
                var items = await Task.Run(() => JsonConvert.DeserializeObject<AuthConfirm>(response, GetJsonSetting()));
            }
            var response1 = await confirmMsg.Content.ReadAsStringAsync();

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(StatePage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            Mobile mobile =  new Mobile()
            { mobile = Mobile };
            var auth = await DataStore.BeneficiaryAuthentication(mobile);
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
    }
}
