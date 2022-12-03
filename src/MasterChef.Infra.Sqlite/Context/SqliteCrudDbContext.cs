using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MasterChef.Infra;
using MasterChef.Infra.Context;

namespace Paghiper.Infra.Sqlite.Context
{
	public class SqliteCrudDbContext : CrudDbContext
	{
		public SqliteCrudDbContext(DbContextOptions options) 
			: base(options) { }
	}
}
