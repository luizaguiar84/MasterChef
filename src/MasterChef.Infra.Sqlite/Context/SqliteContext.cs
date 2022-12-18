using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Sqlite.Context
{
	public class SqliteContext : MasterChef.Infra.Context.DatabaseContext
	{
		public SqliteContext(DbContextOptions options) 
			: base(options) { }
	}
}
