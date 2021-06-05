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
    public class FindByPinViewModel : BaseViewModel
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

        private string pincode;
        public string Pincode
        {
            get => pincode;
            set => SetProperty(ref pincode, value);
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

        #endregion

        #region Constructor

        public FindByPinViewModel()
        {
            Title = "FindByPin";
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
                var search = await DataStore.FindByPin("IN", Pincode, SelectedDate.ToString("dd-MM-yyyy"));
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
