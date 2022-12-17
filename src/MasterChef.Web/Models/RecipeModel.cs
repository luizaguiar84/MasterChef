using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MasterChef.Web.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de título")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de descrição")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo de modo de fazer")]
        public string? WayOfPrepare { get; set; }

        public string? Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public IEnumerable<IngredientModel>? Ingredients { get; set; }
        public List<RecipeModel>? Recipes { get; set; }

        [Required(ErrorMessage = "Por favor selecione um Arquivo")]
        public IFormFile File { get; set; }
    }
}
