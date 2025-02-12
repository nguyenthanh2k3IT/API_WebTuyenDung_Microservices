using Job.Application.Dtos.JobDtos;

namespace Job.Application.Dtos;

public class ApplicantDto
{
    public string Fullname { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public ApplicantStatusDto? Status { get; set; }
    public JobOverviewDto? Job { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
