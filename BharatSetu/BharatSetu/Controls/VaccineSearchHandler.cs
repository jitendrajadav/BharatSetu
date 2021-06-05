using BharatSetu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BharatSetu.Controls
{
    public class VaccineSearchHandler : SearchHandler
    {
        public IList<Session> Vaccines { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }
        public string SearQuery { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            ItemsSource = string.IsNullOrWhiteSpace(newValue)
                ? null
                : (System.Collections.IEnumerable)Vaccines
                    .Where(session => session.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList();
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(1000);
            _ = (Application.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"{GetNavigationTarget()}?name={((Session)item).Name}");
        }

        private string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).CurrentPage.Title;//(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}
