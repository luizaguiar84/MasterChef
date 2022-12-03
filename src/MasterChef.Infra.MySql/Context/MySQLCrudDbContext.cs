using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.MySql.Context
{
	public class MySqlCrudDbContext : CrudDbContext
	{
		public MySqlCrudDbContext(DbContextOptions options)
			: base(options) { }
	}
}
