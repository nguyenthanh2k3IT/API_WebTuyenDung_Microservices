namespace Job.Infrastructure.Data.Configurations;

public class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
{
    public void Configure(EntityTypeBuilder<WorkType> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
