namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenderController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public GenderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.Genders.GetAllList<GenderDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.Genders.GetSlugOneRecord<GenderDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var data = await _unitOfWork.Genders.GetOneRecord<GenderDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.Genders.GetFilterList<GenderDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.Genders.GetPaginatedList<GenderDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Gender request)
    {
        request.ModifiedUser = GetUserId();
        var data = await _unitOfWork.Genders.UpdateAsync(request);
        return ReturnResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Gender request)
    {
        request.CreatedUser = GetUserId();
        var data = await _unitOfWork.Genders.CreateAsync(request);
        return ReturnResponse(data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.Genders.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}
