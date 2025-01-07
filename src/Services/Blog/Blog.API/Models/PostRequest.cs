using BuildingBlock.Core.Enums;
using BuildingBlock.Core.Request;

namespace Blog.API.Models;

public class PostRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public Guid CategoryId { get; set; }
    public PostStatusEnum StatusId { get; set; }
    public List<Guid>? TagNames { get; set; }
}
