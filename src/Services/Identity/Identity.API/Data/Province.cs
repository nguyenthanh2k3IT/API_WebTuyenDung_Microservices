namespace Identity.API.Data;

[Table("tb_provinces")]
public class Province : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Area { get; set; }
    public string AreaName { get; set; }
    public ICollection<CompanyInfo>? CompanyInfos { set; get; }
}
