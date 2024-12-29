using Identity.API.Features.CompanyFeature.Commands;
using Identity.API.Features.CompanyFeature.Queries;
using Identity.API.Models.CompanyModel;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Company_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new Company_GetByIdQuery(id)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Company_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Company_GetPaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CompanyUpdateRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Company_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CompanyAddRequest request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Company_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Company_DeleteCommand(request)));
        }
    }
}
