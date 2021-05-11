using BharatSetu.Models;
using BharatSetu.Views;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class FindByDistrictViewModel : BaseViewModel
    {
        public ObservableCollection<Session> Items { get; }


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
        public Command SearchCommand { get;  }

        public FindByDistrictViewModel()
        {
            Title = "FindByDistrict";
            Items = new ObservableCollection<Session>();
            SearchCommand = new Command(OnSearchClicked);
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnSearchClicked(object obj)
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Test",
                Description = "Test Description",
                ReturningData = "Dummy data", // Returning data when tapped on notification.
                NotifyTime = DateTime.Now.AddSeconds(30) // Used for Scheduling local notification, if not specified notification will show immediately.
            };
            NotificationCenter.Current.Show(notification);

            IsBusy = true;
            try
            {
                var search = await DataStore.FindByDistrict("IN", DistrictId, SelectedDate.ToString("dd-MM-yyyy"));
                if (search.IsSuccessStatusCode)
                {
                    IsVaccinationLoaded = true;
                    var response = await search.Content.ReadAsStringAsync();
                    var items = await Task.Run(() => JsonConvert.DeserializeObject<VaccinationSessions>(response, GetJsonSetting()));
                    Items.Clear();
                    foreach (var item in items.Sessions)
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

        async void OnItemSelected(Session item)
        {
            if (item == null)
                return;

            // This will push the DistrictsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DistrictsPage)}?{nameof(DistrictsViewModel.StateId)}={item.Session_id}");
        }
    }
}
