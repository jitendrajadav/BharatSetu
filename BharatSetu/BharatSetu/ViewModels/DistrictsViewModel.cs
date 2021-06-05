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
    [QueryProperty(nameof(StateId), nameof(StateId))]

    public class DistrictsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Districts> Items => new ObservableCollection<Districts>();

        public string StateId { get; set; }

        private Districts _selectedItem;
        public Districts SelectedItem
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

        public Command AddItemCommand => new Command(OnAddItem);
        public Command<Districts> ItemTapped => new Command<Districts>(OnItemSelected);
        public Command LoadItemsCommand => new Command(async () => await ExecuteLoadItemsCommand());

        #endregion

        #region Constructor

        public DistrictsViewModel()
        {
            Title = "Districts";
        }

        #endregion

        #region Methods

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                var states = await DataStore.Districts("IN", StateId);
                if (states.IsSuccessStatusCode)
                {
                    var response = await states.Content.ReadAsStringAsync();
                    var items = await Task.Run(() => JsonConvert.DeserializeObject<DistrictsModel>(response, GetJsonSetting()));

                    Items.Clear();
                    foreach (var item in items.Districts)
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

        private async void OnItemSelected(Districts item)
        {
            if (item == null)
            {
                return;
            }

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.State_id}");
        }

        #endregion
    }
}
