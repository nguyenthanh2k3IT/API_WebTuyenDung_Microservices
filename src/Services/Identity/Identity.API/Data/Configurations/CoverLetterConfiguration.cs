using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class CoverLetterConfiguration : IEntityTypeConfiguration<CoverLetter>
{
	public void Configure(EntityTypeBuilder<CoverLetter> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
        builder.HasOne(t => t.User).WithMany(t => t.CoverLetters).HasForeignKey(t => t.UserId);
    }
}
