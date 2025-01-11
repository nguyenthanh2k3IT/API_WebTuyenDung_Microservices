namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvinceController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Code" };
    public ProvinceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.Provinces.GetAllList<ProvinceDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var data = await _unitOfWork.Provinces.GetOneRecord<ProvinceDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.Provinces.GetFilterList<ProvinceDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.Provinces.GetPaginatedList<ProvinceDto>(request, searchColumns);
        return ReturnResponse(data);
    }
}
