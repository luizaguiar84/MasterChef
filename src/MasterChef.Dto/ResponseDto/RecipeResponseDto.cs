using MasterChef.Domain.Entities;

namespace MasterChef.Dto.ResponseDto
{
    public class RecipeResponseDto
    {
        public int? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastChange { get; set; }
        public bool Active { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? WayOfPrepare { get; set; }
        public ICollection<Ingredient>? Ingredients { get; set; }
        public string? Image { get; set; }
        public int? UserId { get; set; }
    }
}
