using System;
using Microsoft.Extensions.Configuration;
using MasterChef.Infra.Enums;

namespace MasterChef.Infra
{
	public class DatabaseConfiguration
	{
		public string ConnectionStringName { get; }
		public string ConnectionString { get; }
		public DatabaseType DatabaseType { get; }

		public DatabaseConfiguration(IConfiguration configuration, bool isProducao)
		{
			ConnectionStringName = configuration["DefaultConnectionString"];
			
			if (isProducao)
			{
				DatabaseType = DatabaseType.MySQLProd;
				ConnectionStringName = DatabaseType.MySQLProd.ToString();
			}
			else if (DatabaseType.Sqlite.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
			{
				DatabaseType = DatabaseType.Sqlite;
			}
			else if (DatabaseType.MySQL.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
			{
				DatabaseType = DatabaseType.MySQL;
			}
			else if (DatabaseType.MySQLProd.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
			{
				DatabaseType = DatabaseType.MySQLProd;
			}
			else if (DatabaseType.Postgres.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
			{
				DatabaseType = DatabaseType.Postgres;
			}
			
			else
			{
				throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");
			}

			ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
		}

	}	
}
