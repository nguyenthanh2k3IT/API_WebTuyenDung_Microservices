namespace Identity.API.Models.UserModel;

public class UserPaginationRequest : PaginationRequest
{
	public RoleEnum? Role { get; set; }
}
