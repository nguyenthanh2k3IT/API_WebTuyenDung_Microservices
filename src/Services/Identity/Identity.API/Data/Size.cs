namespace Identity.API.Data;

[Table("tb_sizes")]
public class Size : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Value { get; set; }
    [JsonIgnore] public ICollection<CompanyInfo>? CompanyInfos { set; get; }
}
