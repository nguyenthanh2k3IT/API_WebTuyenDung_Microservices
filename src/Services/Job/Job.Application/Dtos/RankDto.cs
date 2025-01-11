namespace Job.Application.Dtos;

public class RankDto : BaseDto<Guid>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Rank, RankDto>();
        }
    }
}
