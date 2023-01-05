using MasterChef.Mobile.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheRecipeView : ContentPage
    {
        public DetalheRecipeView()
        {
            InitializeComponent();
            BindingContext = new RecipeViewModel();
        }


    }
}