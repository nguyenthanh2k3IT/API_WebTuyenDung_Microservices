using Blog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
	public void Configure(EntityTypeBuilder<Status> builder)
	{
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
