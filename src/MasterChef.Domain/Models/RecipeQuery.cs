namespace MasterChef.Domain.Models;

public class RecipeQuery : RequestDto
{
    public int? UserId { get; set; }
    
    public RecipeQuery(
        int page, 
        int pageSize,
        int? userId) : base(page, pageSize)
    {
        UserId = userId;
    }
}