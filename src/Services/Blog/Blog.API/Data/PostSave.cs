using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.API.Data;

[Table("tb_postSaves")]
public class PostSave : BaseEntity<Guid>
{
    public PostSave() : base() { }
    public Post Post { get; set; }
    [Required] public Guid PostId { get; set; }
    [Required] public Guid UserId { get; set; }
}
