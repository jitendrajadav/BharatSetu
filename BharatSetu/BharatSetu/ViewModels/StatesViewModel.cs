using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class StatesViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<State> Items => new ObservableCollection<State>();

        private State _selectedItem;
        public State SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        #endregion

        #region Commands

        public Command LoadItemsCommand => new Command(async () => await ExecuteLoadItemsCommand());
        public Command AddItemCommand => new Command(OnAddItem);
        public Command<State> ItemTapped => new Command<State>(OnItemSelected);

        #endregion

        #region Constructor

        public StatesViewModel()
        {
            Title = "States";
        }

        #endregion

        #region Methods

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                var states = await DataStore.States("IN");
                if (states.IsSuccessStatusCode)
                {
                    var response = await states.Content.ReadAsStringAsync();
                    var items = await Task.Run(() => JsonConvert.DeserializeObject<StateModel>(response, GetJsonSetting()));

                    Items.Clear();
                    foreach (var item in items.States)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }


        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(State item)
        {
            if (item == null)
                return;

            // This will push the DistrictsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DistrictsPage)}?{nameof(DistrictsViewModel.StateId)}={item.State_id}");
        }

        #endregion    
    }
}
