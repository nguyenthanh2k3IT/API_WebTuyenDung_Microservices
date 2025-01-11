namespace Job.Application.Dtos;

public class GenderDto : BaseDto<Guid>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Gender, GenderDto>();
        }
    }
}
