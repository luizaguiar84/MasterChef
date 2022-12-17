namespace MasterChef.Domain.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Weight { get; set; }
    public int Quantity { get; set; }
    public int RecipeId { get; set; }
}