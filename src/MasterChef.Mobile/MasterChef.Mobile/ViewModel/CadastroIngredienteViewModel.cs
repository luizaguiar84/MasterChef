using MasterChef.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterChef.Mobile.ViewModel
{
    public   class CadastroIngredienteViewModel : BaseViewModel
    {
        private IngredienteModel model;

        public IngredienteModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        public CadastroIngredienteViewModel()
        {

        }

        public CadastroIngredienteViewModel(int id)
        {

            Model = new IngredienteModel();
            Model.RecipeId= id;
        }

        public CadastroIngredienteViewModel(IngredienteModel model)
        {
            IsBusy = false;

            Model = model;
        }
        public IngredienteModel CadastrarIngrediente(IngredienteModel model)
        {
            if (model != null)
            {
                var dados = ingredientesService.CadastrerIngrediente(model);
                return model;

            }
            return model;
        }
    }
}
