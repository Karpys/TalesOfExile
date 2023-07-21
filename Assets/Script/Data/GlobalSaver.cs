using System.Collections.Generic;
using KarpysDev.Script.Items;
using UnityEngine;

namespace Script.Data
{
    public class GlobalSaver : MonoBehaviour
    {
        private static List<ISaver> m_Savers = new List<ISaver>();

        public static void AddSaver(ISaver saver)
        {
            m_Savers.Add(saver);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SaveGame();
            }
        }

        private void SaveGame()
        {
            foreach (ISaver saver in m_Savers)
            {
                saver.WriteSaveData(saver.GetSaveName,saver.FetchSaveData());
            }
        }
    }
}