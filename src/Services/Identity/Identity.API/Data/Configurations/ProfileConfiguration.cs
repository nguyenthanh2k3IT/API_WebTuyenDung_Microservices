using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
	public void Configure(EntityTypeBuilder<Profile> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
        builder.HasOne(t => t.User).WithMany(t => t.Profiles).HasForeignKey(t => t.UserId);
    }
}
