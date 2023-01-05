namespace MasterChef.Dto.ResponseDto
{
    public class IngredientResponseDto
    {
        public int? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastChange { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public int? Quantity { get; set; }
        public int? RecipeId { get; set; }
    }
}
