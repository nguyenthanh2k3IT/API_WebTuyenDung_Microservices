using Blog.API.Features.CategoryFeature.Commands;
using Blog.API.Features.CategoryFeature.Queries;
using Blog.API.Features.PostFeature.Commands;
using Blog.API.Features.PostFeature.Queries;
using Blog.API.Models;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Post_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new Post_GetByIdQuery(id)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Post_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PostPaginationRequest request)
        {
            return Ok(await Mediator.Send(new Post_PaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PostRequest request)
        {
            request.CreatedUser= GetUserId();
            return Ok(await Mediator.Send(new Post_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostRequest request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Post_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Post_DeleteCommand(request)));
        }
    }
}
