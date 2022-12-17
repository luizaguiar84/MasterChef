using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterChef.Domain.Entities;

public class Recipe : BaseEntity
{
    [Display(Name = "Titulo")]
    public string Title { get; set; }
    
    [Display(Name = "Descrição")]
    public string Description { get; set; }
    [Display(Name = "Modo de Preparo")]
    [DataType(DataType.MultilineText)]
    public string WayOfPrepare { get; set; }
    
    [Display(Name = "Ingredientes")]
    public ICollection<Ingredient> Ingredients { get; set; }
    
  
    [Display(Name = "Imagem")]
    public string Picture { get; set; }

}

