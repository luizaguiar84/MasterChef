using MasterChef.Mobile.Model;
using System.Collections.Generic;

namespace MasterChef.Mobile.Interface
{
    public interface IIngredientesService
    {
        List<IngredienteModel> GetById(int id);
        List<RecipeModel>  MontarIngredientes(List<RecipeModel> recipe);
        bool  AtualizarIngrediente(IngredienteModel model);
        bool  CadastrerIngrediente(IngredienteModel model);
        bool  Deletar(int id);
    }
}
