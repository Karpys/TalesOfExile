using System;
using System.Linq;
using KarpysDev.Script.Items;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace Script.Data
{
    public class PlayerDataHolder : MonoBehaviour,ISaver
    {
        [SerializeField] private string m_PlayerDataSaveName = null;
        [SerializeField] private TextAsset m_PlayerDataBaseSave = null;
        private PlayerData m_PlayerData = null;

        private void Awake()
        {
            GlobalSaver.AddSaver(this);
            string[] playerDataSave = SaveUtils.ReadData(GetSaveName, m_PlayerDataBaseSave);
            m_PlayerData = PlayerData.FromSave(playerDataSave);
        }

        public void ChangeGoldValue(float goldValue)
        {
            m_PlayerData.ChangeGoldValue(goldValue);
        }


        public string GetSaveName => m_PlayerDataSaveName;

        public string[] FetchSaveData()
        {
            return m_PlayerData.GetSave();
        }

        public void WriteSaveData(string saveName, string[] data)
        {
            SaveUtils.WriteSave(saveName,data);
        }
    }

    public class PlayerData
    {
        private float m_GoldCount = 0;
        
        private PlayerData(float goldCount)
        {
            m_GoldCount = goldCount;
        }

        public void ChangeGoldValue(float goldValue)
        {
            m_GoldCount += goldValue;
        }

        public string[] GetSave()
        {
            return new string[] {m_GoldCount+""};
        }

        public static PlayerData FromSave(string[] saveData)
        {
            int goldCount = saveData[0].ToInt();
            return new PlayerData(goldCount);
        }
    }

    interface IJsonSavable<T>
    {
        public string ToJson();
        public T FromJson(string json);
    }
}