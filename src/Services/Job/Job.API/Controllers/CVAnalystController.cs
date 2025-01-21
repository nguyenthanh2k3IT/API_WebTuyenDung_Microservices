using Job.Application.Requests;
using Job.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVAnalystController : ControllerBase
    {
        private readonly CVAnalyzerService _cVAnalyzerService;
        public CVAnalystController (CVAnalyzerService cVAnalyzerService)
        {
            _cVAnalyzerService = cVAnalyzerService;
        }
        [HttpPost("analyze")]
        public IActionResult AnalyzeCV([FromBody] CVAnalysisRequest request)
        {
            if (string.IsNullOrEmpty(request.CVContent) || string.IsNullOrEmpty(request.JobDescription))
                return BadRequest("CV and Job Description are required.");
            var result = _cVAnalyzerService.AnalyzeCV(request.CVContent, request.JobDescription);
            return Ok(result);
        }

    }
}
