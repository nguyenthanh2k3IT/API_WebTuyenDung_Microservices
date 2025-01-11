namespace Job.Application.Dtos;

public class ExperienceDto : BaseDto<Guid>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Experience, ExperienceDto>();
        }
    }
}
