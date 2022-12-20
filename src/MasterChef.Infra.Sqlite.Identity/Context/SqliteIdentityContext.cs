using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Sqlite.Identity.Context;

public class SqliteIdentityContext : IdentityDbContext
{
    public SqliteIdentityContext(DbContextOptions<SqliteIdentityContext> options)
        : base(options)
    {
    }
}