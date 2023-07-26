using Nicknotnutils.Extension;

namespace ImageApi;

public static class FileManager
{
    public static List<string> AminPaths = new();
    private static string _aminPath = "/images/amin";
    public static string wwwrootPath = "";
    public static void InitFileManager()
    {
        var files = Directory.GetFiles(wwwrootPath + _aminPath, "*.png", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            AminPaths.Add(file.Replace(wwwrootPath, ""));
        }
    }
}