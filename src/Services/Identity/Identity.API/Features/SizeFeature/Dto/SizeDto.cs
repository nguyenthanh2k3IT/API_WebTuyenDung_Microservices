namespace Identity.API.Features.SizeFeature.Dto;

public class SizeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Size, SizeDto>();
        }
    }
}
