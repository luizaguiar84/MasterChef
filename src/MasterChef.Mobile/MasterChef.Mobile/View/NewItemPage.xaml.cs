using MasterChef.Mobile.Model;
using MasterChef.Mobile.ViewModel;
using Xamarin.Forms;

namespace MasterChef.Mobile.View
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