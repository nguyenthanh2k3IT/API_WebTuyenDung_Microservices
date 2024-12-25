namespace Identity.API.Data;

[Table("tb_companies")]
public class CompanyInfo : BaseEntity<Guid>
{
    public string Wallpaper { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public User User { get; set; }
    public Guid? SizeId { get; set; }
    public Size? Size { get; set; }
    public ICollection<Province> Provinces { set; get; }
}
