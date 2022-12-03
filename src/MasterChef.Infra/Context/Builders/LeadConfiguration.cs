using MasterChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterChef.Infra.Context.Builders
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>

    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Lead");
            builder.HasKey(l => l.Id);
        }
    }
}