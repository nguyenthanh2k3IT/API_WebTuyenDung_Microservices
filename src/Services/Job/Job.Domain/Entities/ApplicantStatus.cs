namespace Job.Domain.Entities;

[Table("tb_applicant_status")]
public class ApplicantStatus : BaseEntity<ApplicantStatusEnum>
{
    public ApplicantStatus() : base() { }
    public string Name { get; set; } = string.Empty;
    public int Sort { get; set; } = 0;
    [JsonIgnore] public ICollection<Applicant>? Applicants { set; get; }
}

