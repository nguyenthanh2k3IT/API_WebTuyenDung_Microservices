﻿using Identity.API.Features.UserFeature.Queries;
using Identity.API.Features.UserFeature.Commands;
using Identity.API.Models.AuthModel;
using Identity.API.Models.UserModel;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
    public class UserController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new User_GetAllQuery(request)));
		}

        [HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new User_GetByIdQuery(id)));
		}

		[HttpGet("filter")]
		public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new User_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		public async Task<IActionResult> Pagination([FromQuery] UserPaginationRequest request)
		{
			return Ok(await Mediator.Send(new User_GetPaginationQuery(request)));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UserAddOrUpdateRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new User_UpdateCommand(request)));
		}

		[HttpPut("status")]
		public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest<UserStatusEnum> request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new User_UpdateStatusCommand(request)));
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] UserAddOrUpdateRequest request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new User_AddCommand(request)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new User_DeleteCommand(request)));
		}

		[HttpPut("profile")]
		public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateRequest request)
		{
			return Ok(await Mediator.Send(new User_UpdateProfileCommand(request)));
		}
	}
}
