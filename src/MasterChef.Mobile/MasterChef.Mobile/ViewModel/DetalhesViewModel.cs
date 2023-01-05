using MasterChef.Mobile.Model;

namespace MasterChef.Mobile.ViewModel
{
    public class DetalhesViewModel : BaseViewModel
    {

        private RecipeModel model;

        public RecipeModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public DetalhesViewModel()
        {
            Model = new RecipeModel();
        }

        public DetalhesViewModel(RecipeModel model)
        {
            IsBusy = false;

            Model = model;


        }
    }
}
