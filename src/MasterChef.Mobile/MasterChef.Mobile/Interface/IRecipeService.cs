using MasterChef.Mobile.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Mobile.Interface
{
    public interface IRecipeService
    {
        List<RecipeModel> GetAll();
        bool Update(RecipeModel recipe);
        bool Save(RecipeModel recipe);
        bool Delete(RecipeModel recipe);
    }
}
