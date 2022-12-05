using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Postgres.Context
{
    public class PostgresContext : Infra.Context.DatabaseContext
    {
        public PostgresContext(DbContextOptions options)
            : base(options) { }
    }
}