using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class CompanyInfoConfiguration : IEntityTypeConfiguration<CompanyInfo>
{
	public void Configure(EntityTypeBuilder<CompanyInfo> builder)
	{
        builder.HasOne(t => t.Size).WithMany(t => t.CompanyInfos).HasForeignKey(t => t.SizeId);
        builder.HasOne(t => t.User).WithOne(t => t.Company).HasForeignKey<CompanyInfo>(t => t.Id);
        builder.HasMany(t => t.Provinces).WithMany(t => t.CompanyInfos)
        .UsingEntity<Dictionary<string, object>>(
            "tb_company_province",
            j => j.HasOne<Province>().WithMany().HasForeignKey("ProvinceId"),
            j => j.HasOne<CompanyInfo>().WithMany().HasForeignKey("CompanyId")); ;
        builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
