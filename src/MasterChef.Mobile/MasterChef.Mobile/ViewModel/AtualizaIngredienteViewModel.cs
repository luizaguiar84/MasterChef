using MasterChef.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterChef.Mobile.ViewModel
{
    public class AtualizaIngredienteViewModel : BaseViewModel
    {
        private IngredientModel model;

        public IngredientModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
     
        public AtualizaIngredienteViewModel()
        {
            Model = new IngredientModel();
        }

        public AtualizaIngredienteViewModel(IngredientModel model)
        {
            IsBusy = false;

            Model = model;
        }

        public IngredientModel AtualizarIngrediente(IngredientModel model)
        {
            if (model != null)
            {
                var dados = IngredientService.Update(model);
                return model;

            }
            return model;
        }
 

        public IngredientModel DeletarIngrediente(IngredientModel model)
        {
            if (model != null)
            {
                var id = Convert.ToInt32(model.Id);
                var dados = IngredientService.Delete(id);
            }
            return model;
        }
    }
}
