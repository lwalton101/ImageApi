using Microsoft.AspNetCore.Mvc;
using Nicknotnutils.Extension;

namespace ImageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AminImageController : ControllerBase
{
    [HttpGet()]
    public ActionResult  Get()
    {
        var data = new { path = FileManager.AminPaths.ChooseRandom() };
        return new JsonResult(data);
    }
}