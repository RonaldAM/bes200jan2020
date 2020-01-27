using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class CacheController : Controller
    {
        IDistributedCache Cache;

        public CacheController(IDistributedCache cache)
        {
            Cache = cache;
        }

        [HttpGet("/time2")]
        [ResponseCache(Duration =15, Location =ResponseCacheLocation.Any)]
        public async Task<ActionResult<string>> GetTime2()
        {
            return Ok($"The Time is {DateTime.Now.ToLongTimeString()}");
        }

        [HttpGet("/time")]
        public async Task<ActionResult<string>> GetTheTime()
        {
            var time = await Cache.GetAsync("time");
            string newTime = null;
            if (time == null)
            {
                newTime = DateTime.Now.ToLongTimeString();
                var encodedTime = Encoding.UTF8.GetBytes(newTime);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));
                await Cache.SetAsync("time", encodedTime, options);
            }
            else
            {
                newTime = Encoding.UTF8.GetString(time);
            }
            return Ok($"It is now {newTime}");
        }
    }
}
