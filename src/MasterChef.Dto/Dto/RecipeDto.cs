namespace MasterChef.Dto.Dto;

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