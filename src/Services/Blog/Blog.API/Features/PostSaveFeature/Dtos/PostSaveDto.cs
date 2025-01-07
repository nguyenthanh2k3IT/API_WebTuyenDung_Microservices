using System.ComponentModel.DataAnnotations;

namespace Blog.API.Features.PostSaveFeature.Dtos;

public class PostSaveDto
{
    [Required] public Guid PostId { get; set; }
    [Required] public Guid UserId { get; set; }
}
