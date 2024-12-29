using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.API.Data;

[Table("tb_categories")]
public class Category : BaseEntity<Guid>
{
    public Category() : base() { }
    [Required] public string Slug { get; set; }

    [Required] [MaxLength(100)]
    public string Name { get; set; }
    [JsonIgnore] public ICollection<Post>? Posts { set; get; }
}
