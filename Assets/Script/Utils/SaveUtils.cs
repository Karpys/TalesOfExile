using System.IO;
using UnityEngine;

public static class SaveUtils
{
    private const string SAVE_DIRECTORY = "/Save/";

    public static string GetSaveDirectory()
    {
        if (Directory.Exists(Application.dataPath + SAVE_DIRECTORY))
        {
            return Application.dataPath + SAVE_DIRECTORY;
        }
        else
        {
            Directory.CreateDirectory(Application.dataPath + SAVE_DIRECTORY);
            return Application.dataPath + SAVE_DIRECTORY;
        }
    }
}
