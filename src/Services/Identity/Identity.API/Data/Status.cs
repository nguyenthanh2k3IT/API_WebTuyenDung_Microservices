using BuildingBlock.Core.Enums;

namespace Identity.API.Data;

[Table("tb_statuses")]
public class Status : BaseEntity<UserStatusEnum>
{
	public Status() : base() {}
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public ICollection<User>? Users { set; get; }
}