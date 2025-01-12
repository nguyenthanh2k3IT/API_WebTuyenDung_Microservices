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

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.Jobs.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}