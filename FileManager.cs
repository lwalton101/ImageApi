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
        var dirs = Directory.GetDirectories(WwwrootPath + "/images", "*", SearchOption.TopDirectoryOnly);
        foreach (var dir in dirs)
        {
            var category = dir.Split("\\").Last();
            ImageCategories.Add(category);
            var files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);
            ImagesByCategory[category] = new List<string>();
            foreach (var file in files)
            {
                ImagesByCategory[category].Add(file.Replace($"{WwwrootPath}/images", $""));
            }
        }
    }
}