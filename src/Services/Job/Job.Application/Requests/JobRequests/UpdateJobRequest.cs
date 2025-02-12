using BuildingBlock.Core.Request;
using Job.Domain.Enums;

namespace Job.Application.Requests.JobRequests;

// Request dành cho admin
public class UpdateJobRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public SalaryType SalaryType { get; set; }
    public decimal SalaryMin { get; set; }
    public decimal SalaryMax { get; set; }
    public int Quantity { get; set; } = 10;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
    public Guid? CategoryId { get; set; }       // Check tồn tại ( 0 có thì throw ex )
    public Guid? RankId { get; set; }           // Check tồn tại ( 0 có thì throw ex )
    public Guid? GenderId { get; set; }         // Check tồn tại ( 0 có thì throw ex )
    public Guid? WorkTypeId { get; set; }       // Check tồn tại ( 0 có thì throw ex )
    public Guid? PopularId { get; set; }        // Check tồn tại ( 0 có thì throw ex )
}
