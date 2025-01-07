using BuildingBlock.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models;

public class CategoriesRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    [Required] public string Slug { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
