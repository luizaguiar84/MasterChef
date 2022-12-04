using MasterChef.Domain.Entities;
using MasterChef.Domain.Entities.Category;
using MasterChef.Domain.Entities.Recipe;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Context
{
	public class Context : DbContext
	{
		
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Category> Categories { get; set; }
		
		public Context()
		{ }

		public Context(DbContextOptions options) : base(options)
		{ }
	}
}
