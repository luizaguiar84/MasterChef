using System.ComponentModel;
using MasterChef.Mobile.ViewModel;
using Xamarin.Forms;

namespace MasterChef.Mobile.View
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