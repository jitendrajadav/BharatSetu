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
        private Districts _selectedItem;

        public ObservableCollection<Districts> Items { get; }
        public Command AddItemCommand { get; }
        public Command<Districts> ItemTapped { get; }

        public Command LoadItemsCommand { get; }

        public string StateId { get; set; }

        public DistrictsViewModel()
        {
            Title = "Districts";
            Items = new ObservableCollection<Districts>();

            ItemTapped = new Command<Districts>(OnItemSelected);
            LoadItemsCommand = new Command(async() => await ExecuteLoadItemsCommand());

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
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

        public Districts SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Districts item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.State_id}");
        }
    }
}
