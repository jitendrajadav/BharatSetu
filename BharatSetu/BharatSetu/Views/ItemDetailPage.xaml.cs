using BharatSetu.ViewModels;
using Xamarin.Forms;

namespace BharatSetu.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}