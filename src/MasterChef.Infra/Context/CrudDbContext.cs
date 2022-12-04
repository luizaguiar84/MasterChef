using MasterChef.Domain.Entities;
using MasterChef.Domain.Entities.Category;
using MasterChef.Domain.Entities.Recipe;
using MasterChef.Infra.Context.Builders;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Context
{
	public class CrudDbContext : DbContext
	{
		
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Category> Categories { get; set; }
		
		public CrudDbContext()
		{ }

		public CrudDbContext(DbContextOptions options) : base(options)
		{ }
	}
}
