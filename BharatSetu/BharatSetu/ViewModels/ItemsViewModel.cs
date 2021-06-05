using BharatSetu.Models;
using BharatSetu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Properties
        public ObservableCollection<Item> Items => new ObservableCollection<Item>();

        private Item _selectedItem;
        public Item SelectedItem
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
        public Command<Item> ItemTapped => new Command<Item>(OnItemSelected);

        #endregion

        #region Constructor

        public ItemsViewModel()
        {
            Title = "Browse";
        }

        #endregion

        #region Methods

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        #endregion
    }
}