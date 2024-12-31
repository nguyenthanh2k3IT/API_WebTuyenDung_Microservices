using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Commands;
using Blog.API.Features.CategoryFeature.Queries;
using Blog.API.Models;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Categories_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new Categories_GetByIdQuery(id)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Categories_GetFillterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Categories_PaginationQuery(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoriesRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Categories_UpdateCommand(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoriesRequest request)
        {
            request.CreatedUser = GetUserId();
            return Ok(await Mediator.Send(new Categories_AddCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new Categories_DeleteCommand(request)));
        }
    }
}
