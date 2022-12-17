using Microsoft.EntityFrameworkCore;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Context
{
    public class UserContext
    {
        public void UserContextConfig(ModelBuilder models)
        {
            models.Entity<User>(x =>
            {
                x.ToTable("Users");
                x.HasKey(c => c.Id).HasName("Id");
                x.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
                x.Property(c => c.Username).HasColumnName("Username").HasMaxLength(100).IsRequired();
                x.Property(c => c.Password).HasColumnName("Password").HasMaxLength(100).IsRequired();
                x.Property(c => c.CreateDate).HasColumnName("CreateDate").HasColumnType("DateTime");
                x.Property(c => c.LastChange).HasColumnName("LastChange").HasColumnType("DateTime");
                x.Property(c => c.Active).HasColumnName("Active").HasColumnType("Bit");
            });
        }
    }
}
