namespace Identity.API.Features.ProvinceFeature.Dto;

public class ProvinceDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Area { get; set; }
    public string AreaName { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Province, ProvinceDto>();
        }
    }
}
