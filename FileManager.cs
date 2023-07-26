using Nicknotnutils.Extension;

namespace ImageApi;

public static class FileManager
{
    public static List<string> AminPaths = new();
    private static string _aminPath = "\\images\\amin";
    public static void InitFileManager(string wwwrootPath)
    {
        var files = Directory.GetFiles(wwwrootPath + _aminPath, "*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            AminPaths.Add(file.Replace(wwwrootPath, ""));
        }
    }
}