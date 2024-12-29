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
    [JsonIgnore] public CompanyInfo? Company { get; set; }
	public RoleEnum? RoleId { get; set; }
    [JsonIgnore] public Role? Role { get; set; }
	public UserStatusEnum? StatusId { get; set; }
    [JsonIgnore] public Status? Status { get; set; }
    [JsonIgnore] public ICollection<Token>? Tokens { set; get; }
    [JsonIgnore] public ICollection<HubConnection>? HubConnections { set; get; }
    [JsonIgnore] public ICollection<OTP>? OTPs { set; get; }
    [JsonIgnore] public ICollection<Notification>? Notifications { set; get; }
    [JsonIgnore] public ICollection<Profile>? Profiles { set; get; }
    [JsonIgnore] public ICollection<CoverLetter>? CoverLetters { set; get; }
}
