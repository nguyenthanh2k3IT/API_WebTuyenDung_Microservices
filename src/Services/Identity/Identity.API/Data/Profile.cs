namespace Identity.API.Data;

[Table("tb_profiles")]
public class Profile : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public string FilePath { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}
