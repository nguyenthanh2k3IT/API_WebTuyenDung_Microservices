namespace Job.Application.Dtos;

public class ApplicantStatusDto : BaseDto<ApplicantStatusEnum>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<ApplicantStatus, ApplicantStatusDto>();
        }
    }
}
