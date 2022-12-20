namespace MasterChef.Dto;

public class RecipeDto
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? WayOfPrepare { get; set; }
    public ICollection<IngredientDto>? Ingredients { get; set; }
    public string? Image { get; set; }
    public int? UserId { get; set; }

    public UserDto? User { get; set; }
}

public class UserDto
{
    public string? Username { get; set; }
    public string? ExternalId { get; set; }
}
public class IngredientDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Weight { get; set; }
    public int? Quantity { get; set; }
    public int? RecipeId { get; set; }
}
