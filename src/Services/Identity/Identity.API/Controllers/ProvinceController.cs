using Identity.API.Features.ProvinceFeature.Commands;
using Identity.API.Features.ProvinceFeature.Queries;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Province_GetAllQuery(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Province_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Province_GetPaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Province request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Province_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Province request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Province_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Province_DeleteCommand(request)));
        }
    }
}