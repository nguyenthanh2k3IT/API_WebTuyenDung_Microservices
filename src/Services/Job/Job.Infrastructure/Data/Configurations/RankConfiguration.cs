namespace Job.Infrastructure.Data.Configurations;

public class RankConfiguration : IEntityTypeConfiguration<Rank>
{
    public void Configure(EntityTypeBuilder<Rank> builder)
    {
        builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
