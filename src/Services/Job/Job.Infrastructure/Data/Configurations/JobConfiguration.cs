using EJob = Job.Domain.Entities.Job;

namespace Job.Infrastructure.Data.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<EJob>
{
    public void Configure(EntityTypeBuilder<EJob> builder)
    {
        builder.HasOne(t => t.Company).WithMany(t => t.Jobs).HasForeignKey(t => t.CompanyId);
        builder.HasOne(t => t.Category).WithMany(t => t.Jobs).HasForeignKey(t => t.CategoryId);
        builder.HasOne(t => t.Rank).WithMany(t => t.Jobs).HasForeignKey(t => t.RankId);
        builder.HasOne(t => t.Gender).WithMany(t => t.Jobs).HasForeignKey(t => t.GenderId);
        builder.HasOne(t => t.Experience).WithMany(t => t.Jobs).HasForeignKey(t => t.ExperienceId);
        builder.HasOne(t => t.WorkType).WithMany(t => t.Jobs).HasForeignKey(t => t.WorkTypeId);
        builder.HasOne(t => t.Popular).WithMany(t => t.Jobs).HasForeignKey(t => t.PopularId);
        builder.HasMany(t => t.WorkLocations).WithMany(t => t.Jobs)
        .UsingEntity<Dictionary<string, object>>(
            "tb_work_locations",
            j => j.HasOne<Province>().WithMany().HasForeignKey("ProvinceId"),
            j => j.HasOne<EJob>().WithMany().HasForeignKey("JobId")); ;
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
