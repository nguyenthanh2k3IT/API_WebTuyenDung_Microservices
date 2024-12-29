using Blog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
	public void Configure(EntityTypeBuilder<Post> builder)
	{
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasIndex(e => e.UserId);
        builder.HasOne(t => t.Category).WithMany(t => t.Posts).HasForeignKey(t => t.CategoryId);
		builder.HasOne(t => t.Status).WithMany(t => t.Posts).HasForeignKey(t => t.StatusId);
        builder.HasMany(t => t.TagNames)
               .WithMany(t => t.Posts)
               .UsingEntity<Dictionary<string, object>>(
                    "tb_post_tagName",
                    j => j.HasOne<TagName>().WithMany().HasForeignKey("TagNameId"),
                    j => j.HasOne<Post>().WithMany().HasForeignKey("PostId"));

        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
