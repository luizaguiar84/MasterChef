﻿using MasterChef.Mobile.Model;
using System.Collections.Generic;

namespace MasterChef.Mobile.Interface
{
    public interface IImageService
    {
        byte[] GetImage(string url);
        bool SaveImage(ImagemModel image);
        List<RecipeModel> MountImage(List<RecipeModel> items);
    }
}
