namespace Identity.API.Data;

[Table("tb_cover_letters")]
public class CoverLetter : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Content { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}
