using MasterChef.Mobile.Model;
using MasterChef.Mobile.ViewModel;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroRecipeView : ContentPage
    {
        public CadastroRecipeView()
        {
            InitializeComponent();

        }

        private void btnSalvar_Clicked(object sender, System.EventArgs e)
        {
            SalvarReceita();
            txtDescricao.Text = "";
            txtTitulo.Text = "";
            edModoPreparo.Text = "";

            Navigation.PushAsync(new DetalheRecipeView());
        }

        public bool SalvarReceita()
        {
            var recipe = new RecipeModel()
            {
                Title = txtTitulo.Text,
                Description = txtDescricao.Text,
                WayOfPrepare = edModoPreparo.Text,
                Image = lblReceita.Text,
                Photo = imgReceita
            };

            AtualizaRecipeViewModel model = new AtualizaRecipeViewModel();
            var fullpath = lblFullPath.Text;
            model.SalvarRecipe(recipe, fullpath);
            return true;
        }
        private async  void btnUpLoad_Clicked(object sender, System.EventArgs e)
        {
             await GetPhoto();
        }

        public async Task<bool> GetPhoto()
        {
            try
            {
                RecipeModel recipe = new RecipeModel();
                await CrossMedia.Current.Initialize();

                var pickImage = await FilePicker.PickAsync(new PickOptions()
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Imagem"
                });
                if (pickImage != null)
                {
                    var steam = await pickImage.OpenReadAsync();
                    imgReceita.Source = ImageSource.FromStream(() => steam);
                    lblReceita.Text = pickImage.FileName;
                    lblFullPath.Text = pickImage?.FullPath;
                }

                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}