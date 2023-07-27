﻿using Nicknotnutils.Extension;

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
            Console.WriteLine(category);
            ImageCategories.Add(category);
            var files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);
            ImagesByCategory[category] = new List<string>();
            foreach (var file in files)
            {
                var catName = file.Split("/").Last();
                Console.WriteLine(catName);
                ImagesByCategory[category].Add(catName);
            }
        }
    }
}