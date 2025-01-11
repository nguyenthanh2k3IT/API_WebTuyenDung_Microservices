namespace Job.Application.Dtos;

public class ProvinceDto : BaseDto<string>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string AreaName { get; set; } = string.Empty;
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Province, ProvinceDto>();
        }
    }
}
