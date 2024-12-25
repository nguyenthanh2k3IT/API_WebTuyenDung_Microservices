namespace Identity.API.Data;

[Table("tb_roles")]
public class Role : BaseEntity<RoleEnum>
{
	public Role() : base() {}
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public ICollection<User>? Users { set; get; }
}

