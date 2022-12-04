using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Postgres.Context
{
    public class PostgresContext : Infra.Context.Context
    {
        public PostgresContext(DbContextOptions options)
            : base(options) { }
    }
}