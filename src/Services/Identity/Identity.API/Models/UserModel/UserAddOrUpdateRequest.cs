namespace Identity.API.Models.UserModel;

public class UserAddOrUpdateRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Fullname { get; set; }
    public string? Password { get; set; }
    public UserStatusEnum? StatusId { get; set; }
    public RoleEnum? RoleId { get; set; }
}
