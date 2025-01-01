using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Features.StatusFeature.Dtos
{
    public class StatusDto
    {
        [Required] public string Slug { get; set; }
        [Required] public string Name { get; set; }
        private class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<Status, StatusDto>();
            }
        }
    }
}
