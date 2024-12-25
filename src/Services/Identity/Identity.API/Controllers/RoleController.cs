using Identity.API.Features.RoleFeature.Commands;
using Identity.API.Features.RoleFeature.Queries;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			var id = GetUserId();
			return Ok(await Mediator.Send(new Role_GetAllQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] RoleEnum id)
		{
			return Ok(await Mediator.Send(new Role_GetByIdQuery(id)));
		}

		[HttpGet("filter")]
		public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new Role_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new Role_GetPaginationQuery(request)));
		}

		[HttpPut]
        public async Task<IActionResult> Update([FromBody] Role request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Role_UpdateCommand(request)));
		}

		[HttpPost]
        public async Task<IActionResult> Add([FromBody] Role request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new Role_AddCommand(request)));
		}

		[HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Role_DeleteCommand(request)));
		}
	}
}
