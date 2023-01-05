using MasterChef.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterChef.Mobile.ViewModel
{
    public class AtualizaIngredienteViewModel : BaseViewModel
    {
        private IngredienteModel model;

        public IngredienteModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
     
        public AtualizaIngredienteViewModel()
        {
            Model = new IngredienteModel();
        }

        public AtualizaIngredienteViewModel(IngredienteModel model)
        {
            IsBusy = false;

            Model = model;
        }

        public IngredienteModel AtualizarIngrediente(IngredienteModel model)
        {
            if (model != null)
            {
                var dados = ingredientesService.AtualizarIngrediente(model);
                return model;

            }
            return model;
        }
 

        public IngredienteModel DeletarIngrediente(IngredienteModel model)
        {
            if (model != null)
            {
                var id = Convert.ToInt32(model.Id);
                var dados = ingredientesService.Deletar(id);
            }
            return model;
        }
    }
}
