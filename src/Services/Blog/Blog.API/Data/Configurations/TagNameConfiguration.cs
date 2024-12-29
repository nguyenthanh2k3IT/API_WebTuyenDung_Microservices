using Blog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class TagNameConfiguration : IEntityTypeConfiguration<TagName>
{
	public void Configure(EntityTypeBuilder<TagName> builder)
	{
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
