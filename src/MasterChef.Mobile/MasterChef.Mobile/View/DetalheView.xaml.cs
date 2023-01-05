using MasterChef.Mobile.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheView : ContentPage
    {
        public DetalheView(DetalhesViewModel view)
        {
            InitializeComponent();
            BindingContext = view;
        }
    }
}