using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public static void WriteSave(string saveName,string[] datas)
    {
        string savePath = GetSaveDirectory() + saveName;
        File.WriteAllLines(savePath,datas);
    }
    
    public static string GetSavePath(string saveName)
    {
        string savePath = GetSaveDirectory() + saveName;
        return savePath;
    }

    public static List<T> InterpretSave<T>(string[] data)
    {
        List<T> saveObjects = new List<T>();
        
        foreach (string objectData in data)
        {
            Type classType = StringUtils.GetTypeViaClassName(objectData.Split()[0]);
            object[] container = new object[1];
            container[0] = objectData.Split();
            saveObjects.Add((T)Activator.CreateInstance(classType,container));
        }

        return saveObjects;
    }
}
