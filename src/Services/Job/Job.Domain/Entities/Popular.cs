namespace Job.Domain.Entities;

[Table("tb_populars")]
public class Popular : BaseEntity<Guid>
{
    public Popular() : base() { }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Background { get; set;} = string.Empty;
    public int TargetApplicants { get; set; } = 0;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}
