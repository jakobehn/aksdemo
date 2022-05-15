using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBoxCore.Api.Models;

namespace QBoxCore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        // GET: api/Health
        [HttpGet]
        public IActionResult Get()
        {
            using (var ctx = new QuizBoxContext())
            {
                if( ctx.Category.Any() )
                    return StatusCode(200);
            }
            return StatusCode(500);
        }

    }
}
