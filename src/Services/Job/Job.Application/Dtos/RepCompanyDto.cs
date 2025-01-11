namespace Job.Application.Dtos;

public class RepCompanyDto : BaseDto<Guid>
{
    public string Fullname { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<RepCompany, RepCompanyDto>();
        }
    }
}
