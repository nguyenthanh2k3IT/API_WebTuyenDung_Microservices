using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.API.Data;

[Table("tb_tagNames")]
public class TagName : BaseEntity<Guid>
{
    [Required] public string Slug { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonIgnore] public ICollection<Post>? Posts { set; get; }
}
