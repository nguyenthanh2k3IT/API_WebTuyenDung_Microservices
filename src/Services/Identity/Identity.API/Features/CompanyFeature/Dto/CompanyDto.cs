using Identity.API.Features.ProvinceFeature.Dto;
using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.CompanyFeature.Dto;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public string Wallpaper { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public SizeDto? Size { get; set; }
    public List<ProvinceDto> Provinces { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<CompanyInfo, CompanyDto>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size ?? null))
                .ForMember(dest => dest.Provinces, opt => opt.MapFrom(src => src.Provinces))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email ?? ""))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone ?? ""))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.Fullname ?? ""))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Avatar ?? ""));
        }
    }
}
