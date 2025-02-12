using BuildingBlock.Core.Request;

namespace Job.Application.Requests.ApplicantRequest;

public class UpdateApplicantRequest : AddOrUpdateRequest
{
    public Guid Id { get; set; }
    public ApplicantStatusEnum StatusId { get; set; }
}
