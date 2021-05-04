using BharatSetu.Models;
using BharatSetu.ViewModels;
using Xamarin.Forms;

namespace BharatSetu.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}