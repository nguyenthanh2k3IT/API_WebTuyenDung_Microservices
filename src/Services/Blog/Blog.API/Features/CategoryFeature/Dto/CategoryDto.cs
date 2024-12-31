using System.ComponentModel.DataAnnotations;

namespace Blog.API.Features.CategoryFeature.Dto
{
    public class CategoryDto
    {
        [Required] public string Slug { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
