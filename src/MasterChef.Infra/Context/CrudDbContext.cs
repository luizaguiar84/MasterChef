using MasterChef.Domain.Entities;
using MasterChef.Infra.Context.Builders;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Context
{
	public class CrudDbContext : DbContext
	{
		
		public DbSet<Lead> Leads { get; set; }
		
		public CrudDbContext()
		{ }

		public CrudDbContext(DbContextOptions options) : base(options)
		{ }

		 protected override void OnModelCreating(ModelBuilder modelBuilder)
		 {
		
		 	modelBuilder.ApplyConfiguration(new LeadConfiguration());
		
		 	base.OnModelCreating(modelBuilder);
		 }
	}
}
