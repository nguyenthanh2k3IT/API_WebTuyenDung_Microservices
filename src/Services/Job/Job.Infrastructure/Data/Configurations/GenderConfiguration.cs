namespace Job.Infrastructure.Data.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
