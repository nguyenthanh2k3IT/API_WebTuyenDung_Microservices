namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public JobController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Test([FromQuery] PaginationRequest request)
    {
        var searchColumns = new[] { "Name", "Slug" };
        return Ok(await _unitOfWork.Categories.GetPaginatedList<CategoryDto>(request, searchColumns));
    }
}