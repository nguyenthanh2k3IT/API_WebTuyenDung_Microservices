using Blog.API.Data;

namespace Blog.API.Features.CategoryFeature.Dto;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
