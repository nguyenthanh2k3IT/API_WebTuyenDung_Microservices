namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PopularController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public PopularController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.Populars.GetAllList<PopularDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.Populars.GetSlugOneRecord<PopularDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var data = await _unitOfWork.Populars.GetOneRecord<PopularDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.Populars.GetFilterList<PopularDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.Populars.GetPaginatedList<PopularDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Popular request)
    {
        request.ModifiedUser = GetUserId();
        var data = await _unitOfWork.Populars.UpdateAsync(request);
        return ReturnResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Popular request)
    {
        request.CreatedUser = GetUserId();
        var data = await _unitOfWork.Populars.CreateAsync(request);
        return ReturnResponse(data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.Populars.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}
