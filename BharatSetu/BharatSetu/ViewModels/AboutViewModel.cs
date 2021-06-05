using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region Commands

        public ICommand OpenWebCommand => new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

        #endregion

        #region Constructor

        public AboutViewModel()
        {
            Title = "About";
        } 

        #endregion
    }
}