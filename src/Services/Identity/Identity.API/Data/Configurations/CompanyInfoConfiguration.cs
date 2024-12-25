using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class CompanyInfoConfiguration : IEntityTypeConfiguration<CompanyInfo>
{
	public void Configure(EntityTypeBuilder<CompanyInfo> builder)
	{
        builder.HasOne(t => t.Size).WithMany(t => t.CompanyInfos).HasForeignKey(t => t.SizeId);
        builder.HasOne(t => t.User).WithOne(t => t.Company).HasForeignKey<CompanyInfo>(t => t.Id);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
