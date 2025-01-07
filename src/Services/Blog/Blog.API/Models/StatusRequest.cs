using BuildingBlock.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models;

public class StatusRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    [Required] public string Slug { get; set; }
    [Required] public string Name { get; set; }
}
