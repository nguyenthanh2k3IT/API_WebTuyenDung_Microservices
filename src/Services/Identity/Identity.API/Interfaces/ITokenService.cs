using Identity.API.Features.AuthFeature.Dto;

namespace Identity.API.Interfaces;

public interface ITokenService
{
	public Task<TokenDto> GenerateToken(Guid id, string email,string fullname,string avatar,string phone, RoleEnum role,UserStatusEnum status);
}
