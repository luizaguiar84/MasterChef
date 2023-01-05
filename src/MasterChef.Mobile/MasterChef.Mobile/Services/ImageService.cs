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
        private IConnectionService service;

        public ImageService(IConnectionService service)
        {
            this.service = service;
        }

        public byte[] GetImage(string url)
        {
            var models = new byte[0];
            var client = service.GetClient();
            var urlApi = service.GetUrl($"/api/imagem/{url}");
            using (var cliente = client)
            {
                cliente.Timeout = new TimeSpan(0, 0, 30);
                cliente.DefaultRequestHeaders.Clear();

                var response = cliente.GetAsync(urlApi);
                if (response.Result.IsSuccessStatusCode)
                {
                    try
                    {
                        var responseString = response.Result.Content.ReadAsStringAsync();
                        models = JsonConvert.DeserializeObject<byte[]>(responseString.Result);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                return models;
            }
        }

        public List<RecipeModel> MontarImagem(List<RecipeModel> items)
        {
            foreach (var item in items)
            {
                var imagemBytes = GetImage(item.Image);
                item.Photo = new Image();
                item.Photo.Source = ImageSource.FromStream(() => { return new MemoryStream(imagemBytes); });
            }
            return items;
        }
        public bool SaveImage(ImagemModel imagem)
        {
            try
            {
                var client = service.GetClient();
                var urlApi = service.GetUrl($"/api/Imagem");
                var content = new StringContent(JsonConvert.SerializeObject(imagem), Encoding.UTF8, "application/json");
                using (var cliente = client)
                {
                    var response = cliente.PostAsync(urlApi, content);
                    if (response.Result.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

