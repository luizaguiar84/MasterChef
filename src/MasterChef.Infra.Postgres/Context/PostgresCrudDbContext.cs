using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Postgres.Context
{
    public class PostgresCrudDbContext : CrudDbContext
    {
        public PostgresCrudDbContext(DbContextOptions options)
            : base(options) { }
    }
}