using BuildingBlock.Core.Request;

namespace Job.Application.Requests.JobRequests;

// Check nếu cái nào != null thì where == 
// ex: if(request.RequestData.CategoryId != null)
//     {
//          query = query.Where(s => s.CategoryId == request.RequestData.CategoryId);
//     }
public class JobPaginationRequest : PaginationRequest
{
    public Guid? CategoryId { get; set; }
    public Guid? RankId { get; set; }
    public Guid? GenderId { get; set; }
    public Guid? WorkTypeId { get; set; }
    public Guid? PopularId { get; set; }
}
