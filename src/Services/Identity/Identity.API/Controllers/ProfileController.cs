using Identity.API.Features.ProfileFeature.Commands;
using Identity.API.Features.ProfileFeature.Queries;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new Profile_GetAllQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new Profile_GetPaginationQuery(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Data.Profile request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Profile_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Profile_DeleteCommand(request)));
        }
    }
}
