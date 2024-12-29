﻿using Identity.API.Features.RoleFeature.Dto;
using Identity.API.Features.StatusFeature.Dto;

namespace Identity.API.Features.UserFeature.Dto;

public class UserDto
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Fullname { get; set; }
	public string? Avatar { get; set; }
	public StatusDto? Status { get; set; }
	public RoleDto? Role { get; set; }
	[JsonIgnore] public UserStatusEnum? StatusId { get; set; }
	[JsonIgnore] public RoleEnum? RoleId { get; set; }
	private class Mapping : AutoMapper.Profile
	{
		public Mapping()
		{
			CreateMap<User, UserDto>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? null))
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? null));
		}
	}
}
