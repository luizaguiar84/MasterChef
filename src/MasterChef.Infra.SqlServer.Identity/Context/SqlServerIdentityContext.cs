using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.SqlServer.Identity.Context;

public class SqlServerIdentityContext : IdentityDbContext
{
    public SqlServerIdentityContext(DbContextOptions<SqlServerIdentityContext> options)
        : base(options)
    {
    }
}