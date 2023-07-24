namespace ImageApi;

public static class FileManager
{
    public static List<string> AminPaths = new();
    private static string _aminPath = "/amin";
    public static void InitFileManager(string path)
    {
        var files = Directory.GetFiles(path + _aminPath, "*", SearchOption.AllDirectories);
        AddArrayToList(AminPaths, files);
    }

    private static void AddArrayToList<T>(List<T> list, T[] array)
    {
        foreach (var VARIABLE in array)
        {
            list.Add(VARIABLE);
        }
    }
}