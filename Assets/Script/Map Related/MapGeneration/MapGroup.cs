using UnityEngine;

[CreateAssetMenu(menuName = "Map/MapPack", fileName = "new MapPAck", order = 0)]
public class MapGroup : ScriptableObject
{
    [SerializeField] private MapGenerationData[] m_Maps = null;

    public MapGenerationData[] MapGenerationData => m_Maps;
}
