using BuildingBlock.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models;

public class PostSaveFilterRequest : FilterRequest
{
    [Required] public Guid PostId { get; set; }
}
