﻿using BharatSetu.Models;
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
        private string distId;
        public string DistId
        {
            get => distId;
            set => SetProperty(ref distId, value);
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

        public CalendarByDistrictViewModel()
        {
            Title = "CalendarByDistrict";
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
                var search = await DataStore.CalanderByDistrict("IN", DistId, SelectedDate.ToString("dd-MM-yyyy"));
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

        async void OnItemSelected(Session item)
        {
            if (item == null)
                return;

            // This will push the DistrictsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DistrictsPage)}?{nameof(DistrictsViewModel.StateId)}={item.Session_id}");
        }
    }
}