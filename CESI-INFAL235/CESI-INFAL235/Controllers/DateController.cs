using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CESI_INFAL235.Controllers
{
    [Route("api/date")]
    [ApiController]
    public class DateController : ControllerBase
    {
        // GET: api/Date
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }
}
