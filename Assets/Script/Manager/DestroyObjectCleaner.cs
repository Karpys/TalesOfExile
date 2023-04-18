using UnityEngine;

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