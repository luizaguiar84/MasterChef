using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MasterChef.Mobile.Model
{
    public class RecipeModel
    {
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("wayOfPrepare")]
        public string WayOfPrepare { get; set; }

        [JsonProperty("ingredients")]
        public ICollection<IngredienteModel> Ingredients { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        public Image Photo { get; set; }
 
    }
}
