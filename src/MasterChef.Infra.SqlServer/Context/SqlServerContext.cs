using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.SqlServer.Context
{
    public class SqlServerContext : DatabaseContext
    {
        public SqlServerContext(DbContextOptions options)
            : base(options) { }
    }
}
