using MasterChef.Mobile.Model;
using System.Collections.Generic;

namespace MasterChef.Mobile.Interface
{
    public interface IIngredientService
    {
        List<IngredientModel> GetById(int id);
        List<RecipeModel> MountIngredients(List<RecipeModel> recipe);
        bool Update(IngredientModel model);
        bool Add(IngredientModel model);
        bool Delete(int id);
    }
}
