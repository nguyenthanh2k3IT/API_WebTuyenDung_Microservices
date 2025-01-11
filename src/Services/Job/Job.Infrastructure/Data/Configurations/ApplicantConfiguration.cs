

namespace Job.Infrastructure.Data.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasOne(t => t.Job).WithMany(t => t.Applicants).HasForeignKey(t => t.JobId);
        builder.HasOne(t => t.Status).WithMany(t => t.Applicants).HasForeignKey(t => t.StatusId);
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
