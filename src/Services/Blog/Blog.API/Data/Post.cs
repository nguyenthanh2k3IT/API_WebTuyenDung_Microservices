using BuildingBlock.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.API.Data;

[Table("tb_posts")]
public class Post : BaseEntity<Guid>
{
    public Post() : base() { }
    [Required] public string Slug { get; set; }

    [Required]
    [MaxLength(250)]
    public string Title { get; set; }
    [Required] public string Content { get; set; }
    [Required] public Guid UserId { get; set; }
    [JsonIgnore] public Status? Status { get; set; }
    public PostStatusEnum? StatusId { get; set; }
    [JsonIgnore] public Category? Category { get; set; }
    public Guid? CategoryId { get; set; }
    [JsonIgnore] public ICollection<PostSave>? PostSaves { set; get; }
    [JsonIgnore] public ICollection<TagName>? TagNames { set; get; }
}
