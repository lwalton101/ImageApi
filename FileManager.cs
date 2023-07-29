using Nicknotnutils.Extension;

namespace ImageApi;

public static class FileManager
{
    private static string _aminPath = "/images/amin";
    public static string WwwrootPath = "";
    public static List<String> ImageCategories = new();
    public static Dictionary<string, List<string>> ImagesByCategory = new();
    public static void InitFileManager()
    {
        ImageCategories = new();
        ImagesByCategory = new();
        var dirs = Directory.GetDirectories(WwwrootPath + "/images", "*", SearchOption.TopDirectoryOnly);
        foreach (var dir in dirs)
        {
            var repalceDir = dir.Replace("\\", "/");
            var category = repalceDir.Split("/").Last();
            ImageCategories.Add(category);
            var files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);
            ImagesByCategory[category] = new List<string>();
            foreach (var file in files)
            {
                
                var fileName = file.Split("/").Last();
                File.Move(file, file.Replace(fileName, fileName.Replace(" ", "_")));
                ImagesByCategory[category].Add(fileName.Replace(" ", "_"));
            }
        }
    }
}