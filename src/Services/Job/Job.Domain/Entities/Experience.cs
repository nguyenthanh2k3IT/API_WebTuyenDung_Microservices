namespace Job.Domain.Entities;

[Table("tb_experiences")]
public class Experience : BaseEntity<Guid>
{
    public Experience() : base() { }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}
