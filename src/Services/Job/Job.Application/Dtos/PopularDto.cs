namespace Job.Application.Dtos;

public class PopularDto : BaseDto<Guid>
{
    public string Background { get; set; } = string.Empty;
    public int TargetApplicants { get; set; } = 0;
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Popular, PopularDto>();
        }
    }
}
