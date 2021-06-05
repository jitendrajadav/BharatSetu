using BharatSetu.Models;
using System;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        #region Properties
        
        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        #endregion

        #region Commands
        
        public Command SaveCommand => new Command(OnSave, ValidateSave);
        public Command CancelCommand => new Command(OnCancel);

        #endregion
     
        #region Constructor

        public NewItemViewModel()
        {
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion

        #region Methods

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(text)
                && !string.IsNullOrWhiteSpace(description);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        #endregion
    }
}
