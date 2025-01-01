using BuildingBlock.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models
{
    public class StatusRequest:AddOrUpdateRequest
    {
        [Required] public string Slug { get; set; }
        [Required] public string Name { get; set; }
    }
}
