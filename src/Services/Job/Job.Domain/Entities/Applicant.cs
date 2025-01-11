namespace Job.Domain.Entities;

[Table("tb_applicants")]
public class Applicant : BaseEntity<Guid>
{
    public Applicant() : base() { }
    public string Fullname { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;

    #region Foreign key
    public Guid? JobId { get; set; }
    public Guid? UserId { get; set; }
    public ApplicantStatusEnum? StatusId { get; set; }
    #endregion

    #region Foreign entities
    public Job? Job { get; set; }
    public ApplicantStatus? Status { get; set; }
    #endregion


}
