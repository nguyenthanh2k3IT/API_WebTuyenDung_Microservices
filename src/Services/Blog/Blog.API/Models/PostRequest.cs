using BuildingBlock.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models
{
    public class PostRequest:AddOrUpdateRequest
    {
        [Required] public string Slug { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required] public string Content { get; set; }
        [Required] public string Image { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
