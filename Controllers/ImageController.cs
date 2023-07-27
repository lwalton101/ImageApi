using ImageApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
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
        var path = $"https://{Request.Host}/images/{imageUrl}";
        var response = new GetImageResponseModel
        {
            category = category,
            imageURL = path
        };

        return new JsonResult(Ok(response));
    }
    
    [HttpGet("{category}/images")]
    public ActionResult GetImages(string category)
    {
        if (!FileManager.ImageCategories.Contains(category))
        {
            return new JsonResult(NotFound($"Cannot find image under category {category}"));
        }

        return new JsonResult(Ok(FileManager.ImagesByCategory[category]));
    }
    
    [HttpPut("{category}/")]
    public ActionResult PutImage(string category,[FromBody] PutImageModel model)
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

        var imgPath = $"{FileManager.WwwrootPath}/images/{category}/{model.ImgName}";
        if (System.IO.File.Exists(imgPath))
        {
            return new JsonResult(Conflict($"File `{model.ImgName}` already exists"));
        }
        System.IO.File.WriteAllBytes(imgPath, bytes);

        return new JsonResult(Ok($"File `{model.ImgName}` successfully created`"));
    }

    [HttpDelete("{category}")]
    public ActionResult DeleteImage([FromBody] DeleteImageModel model, string category)
    {
        var path = $"{FileManager.WwwrootPath}/images/{category}/{model.ImageName}";
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            FileManager.InitFileManager();
            return new JsonResult(Ok($"Deleted Image {model.ImageName}"));
        }

        return new JsonResult(NotFound("Cannot find the image"));
    }

    [HttpGet("categories")]
    public ActionResult GetCategories()
    {
        return new JsonResult(FileManager.ImageCategories);
    }
    [HttpPut("category")]
    public ActionResult CreateCategory([FromBody] PutCategoryModel model)
    {
        if (Directory.Exists(FileManager.WwwrootPath + "\\images\\" + model.CategoryName))
        {
            return new JsonResult(UnprocessableEntity($"{model.CategoryName} already exists"));
        }

        Directory.CreateDirectory($"{FileManager.WwwrootPath}\\images\\{model.CategoryName}");
        FileManager.InitFileManager();
        return new JsonResult(Ok($"Created Category '{model.CategoryName}'"));
    }

    [HttpDelete("category")]
    public ActionResult DeleteCategory([FromBody] DeleteCategoryModel model)
    {
        var path = $"{FileManager.WwwrootPath}\\images\\{model.CategoryName}";
        if (!Directory.Exists(path))
        {
            return new JsonResult(NotFound($"Cannot find Category '{model.CategoryName}'"));
        }
        
        Directory.Delete(path, true);
        FileManager.InitFileManager();
        return new JsonResult(Ok($"Deleted category `{model.CategoryName}`"));
    }
}