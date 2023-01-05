using MasterChef.Mobile.Model;
using System.Collections.Generic;

namespace MasterChef.Mobile.Interface
{
    public interface IImagemService
    {
        byte[] GetImage(string url);
        bool SaveImage(ImagemModel imagem);
        List<RecipeModel> MontarImagem(List<RecipeModel> items);
    }
}
