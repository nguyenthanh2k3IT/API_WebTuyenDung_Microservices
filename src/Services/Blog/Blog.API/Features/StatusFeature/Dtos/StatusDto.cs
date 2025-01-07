using Blog.API.Data;
using BuildingBlock.Core.Enums;

namespace Blog.API.Features.StatusFeature.Dtos;

public class StatusDto
{
    public PostStatusEnum Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Status, StatusDto>();
        }
    }
}
