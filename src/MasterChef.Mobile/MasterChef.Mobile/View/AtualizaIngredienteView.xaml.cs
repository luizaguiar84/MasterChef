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
    public partial class AtualizaIngredienteView : ContentPage
    {
        public AtualizaIngredienteView(AtualizaIngredienteViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            int id = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
            IngredienteModel model = new IngredienteModel()
            {
                Id = id
            };

            AtualizaIngredienteViewModel viewModel = new AtualizaIngredienteViewModel();
            viewModel.DeletarIngrediente(model);
            IngredientsViewModel ingredients = new IngredientsViewModel(model.RecipeId);
            await App.Current.MainPage.Navigation.PushAsync(new IngredientsDetalheView(ingredients));
            limpar();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            int id = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
            IngredienteModel model = new IngredienteModel()
            {
                Id = id,
                RecipeId = lblIdRecipe.Text == "" ? 0 : Convert.ToInt32(lblIdRecipe.Text),
                Name = txtNome.Text,
                Weight = txtPeso.Text == "" ? 0 : Convert.ToDecimal(txtPeso.Text),
                Quantity = txtQuantidade.Text == "" ? 0 : Convert.ToInt32(txtQuantidade.Text),

            };

            AtualizaIngredienteViewModel viewModel = new AtualizaIngredienteViewModel();
            viewModel.AtualizarIngrediente(model);
            IngredientsViewModel ingredients = new IngredientsViewModel(model.RecipeId);
            await App.Current.MainPage.Navigation.PushAsync(new IngredientsDetalheView(ingredients));

            limpar();
        }

        private async void btnCadastrar_Clcked(object sender, EventArgs e)
        {
            var id = lblIdRecipe.Text == "" ? 0 : Convert.ToInt32(lblIdRecipe.Text);
            CadastroIngredienteViewModel viewModel = new CadastroIngredienteViewModel(id);
            await App.Current.MainPage.Navigation.PushAsync(new CadastroIngredienteView(viewModel));

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