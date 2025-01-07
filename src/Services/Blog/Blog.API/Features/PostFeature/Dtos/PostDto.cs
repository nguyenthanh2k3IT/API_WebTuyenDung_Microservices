using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Features.StatusFeature.Dtos;
using Blog.API.Features.TagNameFeature.Dtos;

namespace Blog.API.Features.PostFeature.Dtos;

public class PostDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public CategoryDto? Category { get; set; }
    public StatusDto? Status { get; set; }
    public List<TagNameDto>? TagNames { get; set; }
    public DateTime? CreatedDate { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? null))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category ?? null))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.TagNames));
        }
    }
}
