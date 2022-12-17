using MasterChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Infra.Context
{
    public class IngredientContext
    {
        public void IngredientContextConfig(ModelBuilder models)
        {
            models.Entity<Ingredient>(x =>
            {
                x.ToTable("Ingredients");
                x.HasKey(c => c.Id).HasName("IdIngredient");
                x.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
                x.Property(c => c.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
                x.Property(c => c.RecipeId).HasColumnName("RecipeId").HasMaxLength(100).IsRequired();
                x.Property(c => c.Description).HasColumnName("Description").HasColumnType("varchar").HasMaxLength(255).IsRequired();
                x.Property(c => c.Quantity).HasColumnName("Quantity").HasColumnType("int").IsRequired();
                x.Property(c => c.Weight).HasColumnName("Weight").HasColumnType("Decimal").HasPrecision(18, 2);
                x.Property(c => c.CreateDate).HasColumnName("CreateDate").HasColumnType("DateTime").IsRequired();
                x.Property(c => c.LastChange).HasColumnName("LastChange").HasColumnType("DateTime").IsRequired();
                x.Property(c => c.Active).HasColumnName("Active").HasColumnType("Bit");
            });
        }
    }
}
