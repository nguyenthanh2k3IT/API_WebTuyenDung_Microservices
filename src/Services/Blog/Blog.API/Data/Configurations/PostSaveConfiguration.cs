using Blog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class PostSaveConfiguration : IEntityTypeConfiguration<PostSave>
{
	public void Configure(EntityTypeBuilder<PostSave> builder)
    {
        builder.HasIndex(e => e.UserId);
        builder.HasOne(t => t.Post).WithMany(t => t.PostSaves).HasForeignKey(t => t.PostId);
        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
