using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MasterChef.Infra;
using MasterChef.Infra.Context;

namespace MasterChef.Infra.Sqlite.Context
{
	public class SqliteContext : MasterChef.Infra.Context.DatabaseContext
	{
		public SqliteContext(DbContextOptions options) 
			: base(options) { }
	}
}
