using BharatSetu.Models;
using BharatSetu.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BharatSetu.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public JsonSerializerSettings GetJsonSetting()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                Converters = new List<JsonConverter> { new CustomIntConverter() }
            };
        }

        public class CustomIntConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) => (objectType == typeof(int));

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JValue jsonValue = serializer.Deserialize<JValue>(reader);

                if (jsonValue.Type == JTokenType.Float)
                {
                    return (int)Math.Round(jsonValue.Value<double>());
                }
                else if (jsonValue.Type == JTokenType.Integer)
                {
                    return jsonValue.Value<int>();
                }

                throw new FormatException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {

            }
        }
    }
}
