using BuildingBlock.Core.Request;

namespace Job.Application.Requests.ApplicantRequest;

public class AddApplicantRequest : AddOrUpdateRequest
{
    public string Fullname { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public Guid JobId { get; set; }
    public Guid UserId { get; set; }
    public ApplicantStatusEnum StatusId { get; set; }
}
