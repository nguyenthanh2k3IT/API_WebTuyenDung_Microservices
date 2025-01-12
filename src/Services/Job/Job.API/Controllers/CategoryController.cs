namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.Categories.GetAllList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.Categories.GetSlugOneRecord<CategoryDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var data = await _unitOfWork.Categories.GetOneRecord<CategoryDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.Categories.GetFilterList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.Categories.GetPaginatedList<CategoryDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Category request)
    {
        request.ModifiedUser = GetUserId();
        var data = await _unitOfWork.Categories.UpdateAsync(request);
        return ReturnResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Category request)
    {
        request.CreatedUser = GetUserId();
        var data = await _unitOfWork.Categories.CreateAsync(request);
        return ReturnResponse(data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.Categories.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}
