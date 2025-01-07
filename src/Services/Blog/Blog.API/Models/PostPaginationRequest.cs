using BuildingBlock.Core.Enums;
using BuildingBlock.Core.Request;

namespace Blog.API.Models;

public class PostPaginationRequest : PaginationRequest
{
    public Guid? CategoryId { get; set; }
    public PostStatusEnum? StatusId { get; set; }
}
