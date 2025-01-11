namespace Job.Domain.Entities;

[Table("tb_categories")]
public class Category : BaseEntity<Guid>
{
    public Category() : base() { }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}
