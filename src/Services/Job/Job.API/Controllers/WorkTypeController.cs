namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTypeController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public WorkTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.WorkTypes.GetAllList<WorkTypeDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.WorkTypes.GetSlugOneRecord<WorkTypeDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var data = await _unitOfWork.WorkTypes.GetOneRecord<WorkTypeDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.WorkTypes.GetFilterList<WorkTypeDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.WorkTypes.GetPaginatedList<WorkTypeDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.WorkTypes.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}
