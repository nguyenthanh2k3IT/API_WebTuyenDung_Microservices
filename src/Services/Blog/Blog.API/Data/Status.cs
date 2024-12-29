using BuildingBlock.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.API.Data;

[Table("tb_status")]
public class Status : BaseEntity<PostStatusEnum>
{
    [Required] public string Slug { get; set; }
    [Required] public string Name { get; set; }
    [JsonIgnore] public ICollection<Post>? Posts { set; get; }
}
