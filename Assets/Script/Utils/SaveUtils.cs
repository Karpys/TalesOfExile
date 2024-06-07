using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    using Items;

    public static class SaveUtils
    {
        private const string SAVE_DIRECTORY = "/Save/";

        private static string m_SaveDirectory = string.Empty;
        
        static SaveUtils()
        {
            m_AsyncSaveThread = new Thread(AsyncSave);
            m_AsyncSaveThread.Start();
            SetSaveDirectory();
        }
        
        private static void SetSaveDirectory()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                m_SaveDirectory =  MobileSavePath();
            }
            else
            {
                m_SaveDirectory = ComputerSavePath();
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
            string savePath = m_SaveDirectory + saveName;
            File.WriteAllLines(savePath,datas);
            Debug.Log("File write at :" + savePath);
        }
        
        private static Thread m_AsyncSaveThread = null;
        private static Queue<SaveData> m_AsyncSaveDataQueue = new Queue<SaveData>();
        
        public static void QueueAsyncSave(string saveName, string[] data)
        {
            m_AsyncSaveDataQueue.Enqueue(new SaveData(saveName,data));   
        }
        private static void AsyncSave()
        {
            while (true)
            {
                if (m_AsyncSaveDataQueue.Count > 0)
                {
                    SaveData saveData = m_AsyncSaveDataQueue.Dequeue();
                    string savePath = m_SaveDirectory + saveData.SaveName;
                    File.WriteAllLines(savePath, saveData.SavaDatas);
                    Debug.Log("File async write at :" + savePath);
                }
            }
            
        }
    
        public static string GetSavePath(string saveName)
        {
            string savePath = m_SaveDirectory + saveName;
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

                DirectoryCheck(savePath.ToDirectory());
                File.WriteAllLines(savePath,data);
            }
        
            return File.ReadAllLines(savePath);
        }

        public static string ReadJson(string saveName,string defaultSave)
        {
            string savePath = GetSavePath(saveName);
        
            if (!File.Exists(savePath))
            {
                string data = defaultSave;
                DirectoryCheck(savePath.ToDirectory());
                File.WriteAllLines(savePath,data.ToSingleArray());
            }
        
            return File.ReadAllLines(savePath)[0];
        }

        public static bool SaveExist(string saveName)
        {
            string savePath = GetSavePath(saveName);

            return File.Exists(savePath);
        }

        private static string ToDirectory(this string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static void DirectoryCheck(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        
        public static Item[] InventorySaveDataToItems(string[] data)
        {
            Item[] items = new Item[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                items[i] = DataToItem(data[i]);
            }

            return items;
        }

        private static Item DataToItem(string data)
        {
            if (data == "")
            {
                return null;
            }
            else
            {
                string[] itemDataSplit = data.Split();
                ObjectType objectType = (ObjectType) itemDataSplit[0].ToInt();

                switch (objectType)
                {
                    case ObjectType.DefaultObject:
                        return new DefaultItem(itemDataSplit);
                    case ObjectType.Equipement:
                        return new EquipementItem(itemDataSplit);
                    case ObjectType.Weapon:
                        return new WeaponItem(itemDataSplit);
                    default:
                        Debug.LogError("Missing object type for current item data :" + data);
                        return new DefaultItem(itemDataSplit);
                }
            }
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

    public struct SaveData
    {
        public string SaveName;
        public string[] SavaDatas;

        public SaveData(string saveName, string[] savaDatas)
        {
            SaveName = saveName;
            SavaDatas = savaDatas;
        }
    }
}