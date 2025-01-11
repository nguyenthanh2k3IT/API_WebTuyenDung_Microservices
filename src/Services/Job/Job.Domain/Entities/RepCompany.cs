namespace Job.Domain.Entities;

[Table("tb_rep_companies")]
public class RepCompany : BaseEntity<Guid>
{
    public RepCompany() { }
    public string Slug { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Job>? Jobs { set; get; }
}