using MasterChef.Mobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView : ContentPage
    {
        public RecipeView()
        {
            InitializeComponent();
            BindingContext = new RecipeViewModel();
        }
    }
}
