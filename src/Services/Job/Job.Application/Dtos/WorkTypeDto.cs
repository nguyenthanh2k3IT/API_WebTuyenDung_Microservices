namespace Job.Application.Dtos;

public class WorkTypeDto : BaseDto<Guid>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<WorkType, WorkTypeDto>();
        }
    }
}
