namespace Identity.API.Data;

[Table("tb_sizes")]
public class Size : BaseEntity<Guid>
{
    public Size() : base() { }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<CompanyInfo>? CompanyInfos { set; get; }
}
