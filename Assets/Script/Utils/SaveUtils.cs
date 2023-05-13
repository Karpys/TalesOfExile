using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public static class SaveUtils
{
    private const string SAVE_DIRECTORY = "/Save/";

    public static string GetSaveDirectory()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return MobileSavePath();
        }
        else
        {
            return ComputerSavePath();
        }
    }

    private static string ComputerSavePath()
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
    
    private static string MobileSavePath()
    {
        if (Directory.Exists(Application.persistentDataPath + SAVE_DIRECTORY))
        {
            return Application.persistentDataPath + SAVE_DIRECTORY;
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_DIRECTORY);
            return Application.persistentDataPath + SAVE_DIRECTORY;
        }
    }

    public static void WriteSave(string saveName,string[] datas)
    {
        string savePath = GetSaveDirectory() + saveName;
        File.WriteAllLines(savePath,datas);
        Debug.Log("File write at :" + savePath);
    }
    
    public static string GetSavePath(string saveName)
    {
        string savePath = GetSaveDirectory() + saveName;

        return savePath;
    }

    public static string[] ReadData(string saveName,TextAsset textField)
    {
        string savePath = GetSavePath(saveName);
        
        if (!File.Exists(savePath))
        {
            string[] data = textField.ToString().Split('\n');
            
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].TrimEnd('\r');
            }

            File.WriteAllLines(savePath,data);
        }
        
        return File.ReadAllLines(savePath);
    }

    public static List<T> InterpretSave<T>(string[] data)
    {
        List<T> saveObjects = new List<T>();
        
        foreach (string objectData in data)
        {
            if (objectData == "none")
            {
                saveObjects.Add(default);
                continue;
            }
            
            Type classType = StringUtils.GetTypeViaClassName(objectData.Split()[0]);
            object[] container = new object[1];
            container[0] = objectData.Split();
            saveObjects.Add((T)Activator.CreateInstance(classType,container));
        }

        return saveObjects;
    }
}

public struct DefaultSave
{
    public string DefaultSaveData;
    public int DefaultSaveSize;

    public DefaultSave(string defaultSaveData, int defaultSaveDataSize)
    {
        DefaultSaveData = defaultSaveData;
        DefaultSaveSize = defaultSaveDataSize;
    }
}
