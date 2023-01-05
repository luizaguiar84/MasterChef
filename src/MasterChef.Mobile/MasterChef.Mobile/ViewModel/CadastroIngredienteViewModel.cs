using MasterChef.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterChef.Mobile.ViewModel
{
    public   class CadastroIngredienteViewModel : BaseViewModel
    {
        private IngredientModel model;

        public IngredientModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        public CadastroIngredienteViewModel()
        {

        }

        public CadastroIngredienteViewModel(int id)
        {

            Model = new IngredientModel();
            Model.RecipeId= id;
        }

        public CadastroIngredienteViewModel(IngredientModel model)
        {
            IsBusy = false;

            Model = model;
        }
        public IngredientModel CadastrarIngrediente(IngredientModel model)
        {
            if (model != null)
            {
                var dados = IngredientService.Add(model);
                return model;

            }
            return model;
        }
    }
}
