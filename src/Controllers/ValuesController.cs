// Copyright (c) DealerVision.com All rights reserved.
// ValuesController.cs in VueCoreStarter

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VueCoreStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<int[]> Get()
        {
            return new int[] {1, 5, 6, 7};

        }
    }
}