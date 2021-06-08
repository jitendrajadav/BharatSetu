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
    public class CalendarByDistrictViewModel : BaseViewModel
    {
        #region Properties

        public string SearchBarPlace => BharatSetuResources.FindByDistrictPage_SearchBar_Placeholder;
        public ObservableCollection<Center> Items { get; set; } = new ObservableCollection<Center>();

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

        private string searchBarText;
        public string SearchBarText
        {
            get => searchBarText;
            set => SetProperty(ref searchBarText, value);
        }

        private Session _selectedItem;
        public Session SelectedItem
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
        public Command ApearCommand => new Command(OnAppearing);
        public Command SearchCommand => new Command(OnSearchClicked);

        #endregion

        #region Constructor

        public CalendarByDistrictViewModel()
        {
            Title = "CalendarByDistrict";
        }

        #endregion

        #region Methods

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
                var search = await DataStore.CalanderByDistrict("IN", SearchBarText, SelectedDate.ToString("dd-MM-yyyy"));
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

        private async void OnItemSelected(Session item)
        {
            if (item == null)
            {
                return;
            }

            // This will push the DistrictsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DistrictsPage)}?{nameof(DistrictsViewModel.StateId)}={item.Session_id}");
        }

        #endregion    
    }
}
