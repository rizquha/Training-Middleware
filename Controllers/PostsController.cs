using System;
using Microsoft.AspNetCore.Mvc;

namespace Training_Middleware.Controllers
{
    [Route("/api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("sip");
        }
    }
}