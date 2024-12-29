using Identity.API.Features.SizeFeature.Commands;
using Identity.API.Features.SizeFeature.Queries;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new Size_GetByIdQuery(id)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetPaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Size request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Size_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Size request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Size_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Size_DeleteCommand(request)));
        }
    }
}
