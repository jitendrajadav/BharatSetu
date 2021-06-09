using BharatSetu.Data;
using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class FindByDistrictViewModel : BaseViewModel
    {
        #region Properties
        private int _tapCount;
        public string SearchBarPlace => BharatSetuResources.FindByDistrictPage_SearchBar_Placeholder;

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
            _tapCount++;

            var list = new List<string>
            {
                typeof(NotificationPage).FullName,
                _tapCount.ToString()
            };

            var serializeReturningData = ObjectSerializer.SerializeObject(list);

            var request = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Test",
                Description = $"Tap Count: {_tapCount}",
                BadgeNumber = _tapCount,
                ReturningData = serializeReturningData,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyAutoCancelTime = DateTime.Now.AddSeconds(10000),
                    NotifyRepeatInterval = TimeSpanExt.ToTimeSpanExt(new TimeSpan(0, 0, 0, 1000)),
                    NotifyTime = DateTime.Now.AddSeconds(12),
                    Repeats = NotificationRepeat.Daily
                }
            };

           await NotificationCenter.Current.Show(request);

            IsBusy = true;
            try
            {
                var search = await DataStore.FindByDistrict("IN", SearchBarText, SelectedDate.ToString("dd-MM-yyyy"));
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

            var location = new Location(model.Lat, model.Long);
            var options = new MapLaunchOptions { Name = model.Name };

            try
            {
                await Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                // No map application available to open
            }
        }

        #endregion    
    }
}
