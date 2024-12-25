using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
	public void Configure(EntityTypeBuilder<Province> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}

