using BuildingBlock.Core.Enums;
using System.Text.Json.Serialization;

namespace BuildingBlock.Core.Request;

public class UpdateStatusRequest<TStatus>
{
	public Guid Id { get; set; }
	public TStatus Status { get; set; }
	[JsonIgnore] public Guid? ModifiedUser { set; get; }
	[JsonIgnore] public RoleEnum? RoleId { set; get; }
}
