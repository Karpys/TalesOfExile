using UnityEngine;

namespace KarpysDev.Script.Manager
{
    using Map_Related;

    public class DestroyObjectCleaner : IMapClean
    {
        private GameObject m_worldObject = null;
    
        public DestroyObjectCleaner(GameObject worldObject)
        {
            m_worldObject = worldObject;
            RegisterToMapCleaner();
        }
    
        public void RegisterToMapCleaner()
        {
            MapCleaner.Instance.RegisterObject(this);
        }

        public void Clean()
        {
            if(m_worldObject)
                Object.Destroy(m_worldObject);
        }
    }

    public class TurnObjectCleaner : IMapClean
    {
        private GameObject m_worldObject = null;
        private ITurn m_Turn = null;
    
        public TurnObjectCleaner(GameObject worldObject,ITurn turn)
        {
            m_worldObject = worldObject;
            m_Turn = turn;
            RegisterToMapCleaner();
        }
    
        public void RegisterToMapCleaner()
        {
            MapCleaner.Instance.RegisterObject(this);
        }

        public void Clean()
        {
            if(m_Turn != null)
                TurnManager.Instance.RemoveTurn(m_Turn);   
            if (m_worldObject)
                Object.Destroy(m_worldObject);
        }
    }
}