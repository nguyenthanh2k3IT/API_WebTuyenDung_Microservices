using Blog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
