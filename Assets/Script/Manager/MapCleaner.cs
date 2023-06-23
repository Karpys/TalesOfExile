using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class MapCleaner:SingletonMonoBehavior<MapCleaner>
    {
        private Stack<IMapClean> m_ObjectToClean = new Stack<IMapClean>();

        public void RegisterObject(IMapClean mapClean)
        {
            m_ObjectToClean.Push(mapClean);
        }

        public void Clean()
        {
            int objectCount = m_ObjectToClean.Count;
            for (int i = 0; i < objectCount; i++)
            {
                IMapClean mapClean = m_ObjectToClean.Pop();
                mapClean.Clean();
            }
        }
    }

    public interface IMapClean
    {
        public void RegisterToMapCleaner();
        public void Clean();
    }
}