using MasterChef.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Context
{
	public class DatabaseContext : DbContext
	{
		
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }
		
		public DatabaseContext()
		{ }

		public DatabaseContext(DbContextOptions options) : base(options)
		{ }
	}
}
