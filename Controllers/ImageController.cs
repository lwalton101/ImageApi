using ImageApi.Model;
using Microsoft.AspNetCore.Mvc;
using Nicknotnutils.Extension;

namespace ImageApi.Controllers;

[ApiController]
[Route("api/image/")]
public class ImageController : ControllerBase
{
    [HttpGet("{category}/")]
    public ActionResult Get(string category)
    {
        if (!FileManager.ImageCategories.Contains(category))
        {
            return new JsonResult(NotFound($"Cannot find image under category {category}"));
        }

        var imageUrl = FileManager.ImagesByCategory[category].ChooseRandom();
        var response = new GetImageModel
        {
            category = category,
            imageURL = imageUrl
        };

        return new JsonResult(Ok(response));
    }

    [HttpGet("categories")]
    public ActionResult GetCategories()
    {
        return new JsonResult(FileManager.ImageCategories);
    }
}