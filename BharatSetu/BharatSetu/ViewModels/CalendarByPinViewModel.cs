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
    public class CalendarByPinViewModel : BaseViewModel
    {
        public ObservableCollection<Center> Items { get; }


        private bool isVaccinationLoaded;

        public bool IsVaccinationLoaded
        {
            get => isVaccinationLoaded;
            set => SetProperty(ref isVaccinationLoaded, value);
        }

        private DateTime selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }
        private string pincode;
        public string Pincode
        {
            get => pincode;
            set => SetProperty(ref pincode, value);
        }

        private Center _selectedItem;
        public Center SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public Command SearchCommand { get;  }

        public CalendarByPinViewModel()
        {
            Title = "CalendarByPin";
            Items = new ObservableCollection<Center>();
            SearchCommand = new Command(OnSearchClicked);
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnSearchClicked(object obj)
        {
            IsBusy = true;
            try
            {
                var search = await DataStore.CalanderByPin("IN", Pincode, SelectedDate.ToString("dd-MM-yyyy"));
                if (search.IsSuccessStatusCode)
                {
                    IsVaccinationLoaded = true;
                    var response = await search.Content.ReadAsStringAsync();
                    var items = await Task.Run(() => JsonConvert.DeserializeObject<CalenderByPin>(response, GetJsonSetting()));
                    Items.Clear();
                    foreach (var item in items.Centers)
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

        async void OnItemSelected(Center item)
        {
            if (item == null)
                return;

            // This will push the DistrictsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DistrictsPage)}?{nameof(DistrictsViewModel.StateId)}={item.Center_id}");
        }
    }
}
