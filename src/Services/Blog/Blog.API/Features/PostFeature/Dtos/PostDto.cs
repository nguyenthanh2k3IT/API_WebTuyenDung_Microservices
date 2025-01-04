using Blog.API.Data;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Features.PostFeature.Dtos
{
    public class PostDto
    {
        [Required] public string Slug { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;
        [Required] public string Content { get; set; } = string.Empty;
        [Required] public string Image { get; set; } = string.Empty;
        [Required] public Guid UserId { get; set; }
        private class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<Post, PostDto>();
            }
        }
    }
}
