namespace Identity.API.Data;

[Table("tb_provinces")]
public class Province : BaseEntity<string>
{
    public Province() : base() { }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string AreaName { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<CompanyInfo>? CompanyInfos { set; get; }
}
