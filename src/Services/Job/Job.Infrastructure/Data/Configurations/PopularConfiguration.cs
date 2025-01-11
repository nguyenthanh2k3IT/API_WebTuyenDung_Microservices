namespace Job.Infrastructure.Data.Configurations;

public class PopularConfiguration : IEntityTypeConfiguration<Popular>
{
    public void Configure(EntityTypeBuilder<Popular> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
