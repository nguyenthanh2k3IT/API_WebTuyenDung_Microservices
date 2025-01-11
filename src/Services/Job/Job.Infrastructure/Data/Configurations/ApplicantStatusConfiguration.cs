namespace Job.Infrastructure.Data.Configurations;

public class ApplicantStatusConfiguration : IEntityTypeConfiguration<ApplicantStatus>
{
    public void Configure(EntityTypeBuilder<ApplicantStatus> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
