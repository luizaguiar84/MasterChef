namespace MasterChef.Domain.Entities.Recipe;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string MeasurementUnit { get; set; }
    public string Quantity { get; set; }
}