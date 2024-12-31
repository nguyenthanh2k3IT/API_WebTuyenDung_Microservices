using Identity.API.Features.CoverLetterFeature.Commands;
using Identity.API.Features.CoverLetterFeature.Queries;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverLetterController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new CoverLetter_GetAllQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new CoverLetter_GetPaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CoverLetter request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new CoverLetter_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CoverLetter request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new CoverLetter_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new CoverLetter_DeleteCommand(request)));
        }
    }
}
