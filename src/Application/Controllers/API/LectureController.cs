using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers.API
{
    [Route("api/lectures")]
    public class LectureController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public LectureController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            string lectures = await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(this.hostingEnvironment.WebRootPath, "lectures", "lectures.json"));

            return Ok(lectures);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("{day}")]
        public async Task<IActionResult> Get(int day)
        {
            string lectureContent = await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(this.hostingEnvironment.WebRootPath, "lectures", $"day{day}.md"));

            return Ok(lectureContent);
        }
    }
}
