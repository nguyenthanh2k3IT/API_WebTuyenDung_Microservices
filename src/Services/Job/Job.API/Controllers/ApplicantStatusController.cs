using BuildingBlock.Core.Enums;

namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicantStatusController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public ApplicantStatusController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.ApplicantStatuses.GetAllList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.ApplicantStatuses.GetSlugOneRecord<CategoryDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] ApplicantStatusEnum id)
    {
        var data = await _unitOfWork.ApplicantStatuses.GetOneRecord<CategoryDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.ApplicantStatuses.GetFilterList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.ApplicantStatuses.GetPaginatedList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }
}
