using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
[ApiController]
[Route("[controller]")]
public class MessagerController : ControllerBase
{  
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hello, World!";
    }
}
