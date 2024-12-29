using BuildingBlock.Core.Enums;
using System.Text.Json.Serialization;

namespace BuildingBlock.Core.Request;

public class AddOrUpdateRequest
{
	[JsonIgnore] public Guid? CreatedUser { set; get; }
	[JsonIgnore] public Guid? ModifiedUser { set; get; }
	[JsonIgnore] public RoleEnum? RoleId { set; get; }
}
