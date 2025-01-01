
using Blog.API.Features.StatusFeature.Commands;
using Blog.API.Features.StatusFeature.Queries;
using Blog.API.Models;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Status_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string Slug)
        {
            return Ok(await Mediator.Send(new Status_GetByIdQuery(Slug)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Status_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Status_GetPaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StatusRequest request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Status_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StatusRequest request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Status_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Status_DeleteCommand(request)));
        }
    }
}
