namespace Job.Domain.Entities;

[Table("tb_work_types")]
public class WorkType : BaseEntity<Guid>
{
    public WorkType() : base() { }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}
