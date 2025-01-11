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

   /* [HttpPut]
    public async Task<IActionResult> Update([FromBody] StatusRequest request)
    {
        request.CreatedUser = GetUserId();
        return Ok(await Mediator.Send(new Status_UpdateCommand(request)));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] StatusRequest request)
    {
        request.CreatedUser = GetUserId();
        return Ok(await Mediator.Send(new Status_AddCommand(request)));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        return Ok(await Mediator.Send(new Status_DeleteCommand(request)));
    }*/
}
