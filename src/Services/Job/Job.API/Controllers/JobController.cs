namespace Job.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : BaseController
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("ok");
    }
}
