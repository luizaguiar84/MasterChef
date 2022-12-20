namespace MasterChef.Dto;

public class IngredientDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Weight { get; set; }
    public int? Quantity { get; set; }
    public int? RecipeId { get; set; }
}