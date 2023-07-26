using Nicknotnutils.Extension;

namespace ImageApi;

public static class FileManager
{
    public static List<string> AminPaths = new();
    private static string _aminPath = "/amin";
    public static void InitFileManager(string path)
    {
        var files = Directory.GetFiles(path + _aminPath, "*", SearchOption.AllDirectories);
        AminPaths.AddArray(files);
    }
}