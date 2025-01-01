using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Features.TagNameFeature.Dtos
{
    public class TagNameDto
    {
        [Required] public string Slug { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        private class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<TagName, TagNameDto>();
            }
        }
    }
}
