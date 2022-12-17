using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Context
{
    public class RecipeContext
    {
        public void RecipeContextConfig(ModelBuilder models)
        {
            models.Entity<Recipe>(x =>
            {
                x.ToTable("Recipes");
                x.HasKey(c => c.Id).HasName("Id");
                x.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
                x.Property(c => c.Title).HasColumnName("Title").HasColumnType("varchar").HasMaxLength(100).IsRequired();
                x.Property(c => c.Description).HasColumnName("Description").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
                x.Property(c => c.WayOfPrepare).HasColumnName("WayOfPrepare").HasColumnType("varchar").HasMaxLength(5000).IsRequired();
                x.Property(c => c.Picture).HasColumnName("Picture").HasMaxLength(250);
                x.Property(c => c.CreateDate).HasColumnName("CreateDate").HasColumnType("DateTime");
                x.Property(c => c.LastChange).HasColumnName("LastChange").HasColumnType("DateTime");
                x.Property(c => c.Active).HasColumnName("Active").HasColumnType("Bit");

            });

        }
    }
}
