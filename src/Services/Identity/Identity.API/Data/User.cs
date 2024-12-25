namespace Identity.API.Data;

[Table("tb_users")]
public class User : BaseEntity<Guid>
{
	public User() : base() 
	{
		Id = Guid.NewGuid();
		Avatar = AvatarConstant.Default;
	}
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string Fullname { get; set; } = string.Empty;
	public string? Avatar { get; set; } = string.Empty;
	public RoleEnum? RoleId { get; set; }
	public Role? Role { get; set; }
	public UserStatusEnum? StatusId { get; set; }
	public Status? Status { get; set; }
	public ICollection<Token>? Tokens { set; get; }
	public ICollection<HubConnection>? HubConnections { set; get; }
	public ICollection<OTP>? OTPs { set; get; }
	public ICollection<Notification>? Notifications { set; get; }
}
