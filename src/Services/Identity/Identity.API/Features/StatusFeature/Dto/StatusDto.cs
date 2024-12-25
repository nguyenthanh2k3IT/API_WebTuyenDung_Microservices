namespace Identity.API.Features.StatusFeature.Dto;

public class StatusDto
{
	public UserStatusEnum Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	private class Mapping : AutoMapper.Profile
	{
		public Mapping()
		{
			CreateMap<Status, StatusDto>();
		}
	}
}
