namespace Job.Infrastructure.Data.Configurations;

public class RepCompanyConfiguration : IEntityTypeConfiguration<RepCompany>
{
    public void Configure(EntityTypeBuilder<RepCompany> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
