using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBox.Api.Client;

namespace QBoxCore.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        static DateTime? startup;

        // GET: api/Health
        [HttpGet]
        public IActionResult Get()
        {
            if (!startup.HasValue)
            {
                startup = DateTime.Now;
            }
            var elapsedTime = DateTime.Now - startup.Value;
            if (elapsedTime.TotalSeconds < 20)
            {
                return StatusCode(500);
            }
            return StatusCode(200);
        }

    }
}
