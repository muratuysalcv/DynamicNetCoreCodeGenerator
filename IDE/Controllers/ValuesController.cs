using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Helpers;

namespace Scriptingo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FastAuthorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [FastAuthorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
