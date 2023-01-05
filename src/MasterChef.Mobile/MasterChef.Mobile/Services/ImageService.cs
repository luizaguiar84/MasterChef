using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MasterChef.Mobile.Services
{
    public class ImageService : IImageService
    {
        private readonly IConnectionService _service;

        public ImageService(IConnectionService service)
        {
            this._service = service;
        }

        public byte[] GetImage(string url)
        {
            var models = Array.Empty<byte>();
            using var client = _service.GetClient();

            var urlApi = _service.GetUrl($"/api/imagem/{url}");
            client.Timeout = new TimeSpan(0, 0, 30);

            var response = client.GetAsync(urlApi);
            if (!response.Result.IsSuccessStatusCode) 
                return models;
            
            try
            {
                var responseString = response.Result.Content.ReadAsStringAsync();
                models = JsonConvert.DeserializeObject<byte[]>(responseString.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return models;
        }

        public List<RecipeModel> MountImage(List<RecipeModel> items)
        {
            foreach (var item in items)
            {
                var bytes = GetImage(item.Image);
                item.Photo = new Image
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(bytes))
                };
            }
            return items;
        }
        public bool SaveImage(ImageModel image)
        {
            try
            {
                using var client = _service.GetClient();

                var urlApi = _service.GetUrl($"/api/Image");
                var content = new StringContent(JsonConvert.SerializeObject(image), Encoding.UTF8, "application/json");
                var response = client.PostAsync(urlApi, content);
                
                return response.Result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

