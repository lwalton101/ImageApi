﻿using ImageApi.Model;
using Microsoft.AspNetCore.Mvc;
using Nicknotnutils.Extension;

namespace ImageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AminImageController : ControllerBase
{

    private readonly ILogger<AminImageController> _logger;

    public AminImageController(ILogger<AminImageController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public ActionResult Get()
    {
        var data = new { path = $"https://{Request.Host}{FileManager.AminPaths.ChooseRandom()}" };
        return new JsonResult(Ok(data));
    }

    [HttpPut()]
    public ActionResult Put([FromBody] CreateAminImageModel model)
    {
        byte[] bytes;
        try
        {
            bytes = Convert.FromBase64String(model.B64Img);
        }
        catch (FormatException)
        {
            return new JsonResult(BadRequest("Valid base64 was not sent"));
        }

        if (!model.ImgName.EndsWith(".png"))
        {
            return new JsonResult(BadRequest("File must be a .png"));
        }

        if (model.B64Img == "")
        {
            return new JsonResult(BadRequest("b64Img must not be null"));
        }

        var imgPath = $"{FileManager.wwwrootPath}/images/amin/{model.ImgName}";
        if (System.IO.File.Exists(imgPath))
        {
            return new JsonResult(Conflict($"File `{model.ImgName}` already exists"));
        }
        System.IO.File.WriteAllBytes(imgPath, bytes);

        return new JsonResult(Ok($"File `{model.ImgName}` successfully created`"));
    }

    [HttpDelete]
    public ActionResult Delete(string name)
    {
        var imgPath = $"{FileManager.wwwrootPath}/images/amin/{name}";
        if (System.IO.File.Exists(imgPath))
        {
            System.IO.File.Delete(imgPath);
            return new JsonResult(Ok($"File `{name}` has been deleted"));
        }

        return new JsonResult(NotFound($"File `{name}` cannot be found"));
    }
}