namespace MasterChef.Domain.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Weight { get; set; }
    public string Quantity { get; set; }
    public int RecipeId { get; set; }
}