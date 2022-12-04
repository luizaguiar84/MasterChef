using System.Collections.Generic;

namespace MasterChef.Domain.Entities.Recipe;

public class Recipe : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<Ingredient> Ingredients { get; set; }
    public string WayOfPrepare { get; set; }
    public string Picture { get; set; }
    public IList<Tag> Tags { get; set; }
    public Category.Category Category { get; set; } 
}

