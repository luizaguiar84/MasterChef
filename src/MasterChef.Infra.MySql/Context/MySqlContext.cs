using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.MySql.Context
{
	public class MySqlContext : Infra.Context.DatabaseContext
	{
		public MySqlContext(DbContextOptions options)
			: base(options) { }
	}
}
