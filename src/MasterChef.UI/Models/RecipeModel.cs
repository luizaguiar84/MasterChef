using System.ComponentModel.DataAnnotations;
using MasterChef.Domain.Entities;

namespace MasterChef.UI.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de título")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de descrição")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de modo de fazer")]
        public string? WayOfPrepare { get; set; }

        public string? Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public IEnumerable<IngredientModel>? Ingredients { get; set; } = new List<IngredientModel>();
        public List<RecipeModel>? Recipes { get; set; } = new List<RecipeModel>();
        public IFormFile? File { get; set; }
        public User? User { get; set; }
    }
}
