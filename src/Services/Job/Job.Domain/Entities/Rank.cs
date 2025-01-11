namespace Job.Domain.Entities;

[Table("tb_ranks")]
public class Rank : BaseEntity<Guid>
{
    public Rank() : base() { }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}
