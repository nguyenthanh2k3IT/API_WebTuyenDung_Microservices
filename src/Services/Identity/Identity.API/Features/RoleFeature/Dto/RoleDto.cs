namespace Identity.API.Features.RoleFeature.Dto;

public class RoleDto
{
	public RoleEnum Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	private class Mapping : AutoMapper.Profile
	{
		public Mapping()
		{
			CreateMap<Role, RoleDto>();
		}
	}
}
