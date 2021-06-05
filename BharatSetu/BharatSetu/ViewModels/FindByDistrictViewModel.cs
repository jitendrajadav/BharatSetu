using BharatSetu.Data;
using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class FindByDistrictViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Session> Items { get; set; } = new ObservableCollection<Session>();

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
        private string districtId;
        public string DistrictId
        {
            get => districtId;
            set => SetProperty(ref districtId, value);
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

        public Command SearchCommand => new Command(OnSearchClicked);
        public Command ItemTapped => new Command<Session>(OnItemTapped);
        public Command FilterCommand => new Command<string>(FilterItems);

        #endregion

        #region Constructor

        public FindByDistrictViewModel()
        {
            Title = "FindByDistrict";
        }

        #endregion

        #region Methods

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private void FilterItems(string filter)
        {
            var filteredItems = Items.Where(session => session.Name.ToLower().Contains(filter.ToLower())).ToList();
            Items = new ObservableCollection<Session>(filteredItems);
        }

        private async void OnSearchClicked(object obj)
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Test",
                Description = "Test Description",
                ReturningData = "Dummy data", // Returning data when tapped on notification.
                BadgeNumber = 1
            };
            bool v = await NotificationCenter.Current.Show(notification);

            IsBusy = true;
            try
            {
                var search = await DataStore.FindByDistrict("IN", DistrictId, SelectedDate.ToString("dd-MM-yyyy"));
                if (search.IsSuccessStatusCode)
                {
                    BharatSetuDB database = await BharatSetuDB.Instance;
                    IsVaccinationLoaded = true;
                    var response = await search.Content.ReadAsStringAsync();
                    var items = await Task.Run(() => JsonConvert.DeserializeObject<VaccinationSessions>(response, GetJsonSetting()));
                    Items.Clear();
                    //Items = new ObservableCollection<Session>(items.Sessions);
                    foreach (Session item in items.Sessions)
                    {
                        Items.Add(item);
                    }

                    await database.SaveAllAsync(items.Sessions);
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

        private async void OnItemTapped(Session model)
        {
            BharatSetuDB database = await BharatSetuDB.Instance;
            _ = await database.GetItemsAsync();
        }

        #endregion    
    }
}
