using Blog.API.Features.PostSaveFeature.Queries;
using Blog.API.Models;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostSaveController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new PostSave_GetAllQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            return Ok(await Mediator.Send(new PostSave_GetByIdQuery(Id)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] PostSaveFilterRequest request)
        {
            return Ok(await Mediator.Send(new PostSave_GetFilterQuery(request)));
        }

      

    }
}
