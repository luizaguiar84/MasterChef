using System;

namespace MasterChef.Mobile.Model
{
    public class IngredienteModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Weight { get; set; }
        public int? Quantity { get; set; }
        public int RecipeId { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
