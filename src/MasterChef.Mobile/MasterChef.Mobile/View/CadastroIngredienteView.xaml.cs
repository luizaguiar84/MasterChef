using MasterChef.Mobile.Model;
using MasterChef.Mobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroIngredienteView : ContentPage
    {
        public CadastroIngredienteView(CadastroIngredienteViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            int id = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
            int recipeId = lblIdRecipe.Text == "" ? 0 : Convert.ToInt32(lblIdRecipe.Text);
            IngredientModel model = new IngredientModel()
            {
                Id = id,
                RecipeId = lblIdRecipe.Text == "" ? 0 : Convert.ToInt32(lblIdRecipe.Text),
                Name = txtNome.Text,
                Weight = txtPeso.Text == "" ? 0 : Convert.ToDecimal(txtPeso.Text),
                Quantity = txtQuantidade.Text == "" ? 0 : Convert.ToInt32(txtQuantidade.Text),

            };

            CadastroIngredienteViewModel viewModel = new CadastroIngredienteViewModel();
            viewModel.CadastrarIngrediente(model);
            IngredientsViewModel ingredients = new IngredientsViewModel(recipeId);
            await App.Current.MainPage.Navigation.PushAsync(new IngredientsDetalheView(ingredients));

            limpar();
        }

        public void limpar()
        {
            lblId.Text = "";
            lblIdRecipe.Text = "";
            txtNome.Text = "";
            txtPeso.Text = "";
            txtQuantidade.Text = "";
        }
    }
}