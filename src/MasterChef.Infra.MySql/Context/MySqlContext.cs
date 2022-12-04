using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.MySql.Context
{
	public class MySqlContext : Infra.Context.Context
	{
		public MySqlContext(DbContextOptions options)
			: base(options) { }
	}
}
