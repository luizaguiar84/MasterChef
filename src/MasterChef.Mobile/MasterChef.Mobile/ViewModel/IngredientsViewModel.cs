using MasterChef.Mobile.Model;
using MasterChef.Mobile.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MasterChef.Mobile.ViewModel
{
    public class IngredientsViewModel : BaseViewModel
    {
        private ObservableCollection<IngredientModel> model;
        public ObservableCollection<IngredientModel> Model
        {
            get { return model; }
            set
            {
                SetProperty(ref model, value);
            }
        }
        private IngredientModel selectedModel;
        public IngredientModel SelectedModel
        {
            get { return selectedModel; }
            set { SetProperty(ref selectedModel, value); }
        }
        public IngredientsViewModel(int id)
        {
            var dados = IngredientService.GetById(id);
            Model = new ObservableCollection<IngredientModel>(dados);
        }
        public ICommand OpenDetalheCommand => new Command<IngredientModel>(async (IngredientModel d) =>
        {
            var vm = new AtualizaIngredienteViewModel(d);
            await App.Current.MainPage.Navigation.PushAsync(new AtualizaIngredienteView(vm));
        });
    }
}
