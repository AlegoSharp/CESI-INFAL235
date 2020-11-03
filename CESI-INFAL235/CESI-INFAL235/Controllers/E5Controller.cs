using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CESI_INFAL235.Controllers
{
    [Route("api/e5")]
    [ApiController]
    public class E5Controller : ControllerBase
    {
        public int smallest { get; set; }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(smallestMultiple());
        }

        private bool Compare(int i,int y)
        {
            var test = this;
            if (i % 20 == 0 &&
                i % 19 == 0 &&
                i % 17 == 0 &&
                i % 16 == 0 &&
                i % 14 == 0 &&
                i % 13 == 0 &&
                i % 12 == 0 &&
                i % 11 == 0 &&
                i % 9 == 0 &&
                i % 7 == 0 &&
                i % 5 == 0 &&
                i % 3 == 0 &&
                i % 2 == 0)
            {

                smallest = i;
                return true;
            }
            else { return false; }
        }
        public int smallestMultiple()
        {
            int i,y;
            int max = 1000000000;
            y = 0;

            for (i = 2520; i < max; i = 2520 + i)
            {
                var th = new Task(() => { y = Compare(i,y) ? i : 0; });
                th.Start();
                if (smallest > 0)
                {
                    return i;
                }                
            }
            return i;
        }

    }
}