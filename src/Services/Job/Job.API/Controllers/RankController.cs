﻿namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RankController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<string> searchColumns = new[] { "Name", "Slug" };
    public RankController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
    {
        var data = await _unitOfWork.Ranks.GetAllList<RankDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var data = await _unitOfWork.Ranks.GetSlugOneRecord<RankDto>(slug);
        return ReturnResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var data = await _unitOfWork.Ranks.GetOneRecord<RankDto>(id);
        return ReturnResponse(data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
    {
        var data = await _unitOfWork.Ranks.GetFilterList<RankDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        var data = await _unitOfWork.Ranks.GetPaginatedList<RankDto>(request, searchColumns);
        return ReturnResponse(data);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Rank request)
    {
        request.ModifiedUser = GetUserId();
        var data = await _unitOfWork.Ranks.UpdateAsync(request);
        return ReturnResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Rank request)
    {
        request.CreatedUser = GetUserId();
        var data = await _unitOfWork.Ranks.CreateAsync(request);
        return ReturnResponse(data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
    {
        request.ModifiedUser = GetUserId();
        var ids = request.Ids.Select(s => Guid.Parse(s)).ToList();
        var res = await _unitOfWork.Ranks.DeleteRecords(ids, GetUserId());
        return ReturnResponse(res);
    }
}
