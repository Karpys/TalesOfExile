using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private ZoneSelection m_LightProjection = null;
    [SerializeField] private float m_LightForce = 1;

    private BoardEntity m_AttachedEntity = null;
    void Start()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateLights();
        }
    }

    private void UpdateLights()
    {
        List<Vector2Int> lightable = ZoneTileManager.GetSelectionZone(m_LightProjection,
            m_AttachedEntity.EntityPosition, m_LightProjection.Range);

        Vector2Int lightOrigin = m_AttachedEntity.EntityPosition;
        
        foreach (Vector2Int pos in lightable)
        {
            Vector2Int clampedPos = MapData.Instance.MapClampedPosition(pos);
            LinePath.NeighbourType = NeighbourType.Cross;
            List<WorldTile> worldTiles = LinePath.GetPathTile(lightOrigin, clampedPos).Select(s => s.WorldTile).ToList();
            LinePath.NeighbourType = NeighbourType.Square;

            foreach (WorldTile worldTile in worldTiles)
            {
                //TODO:ADD Bool Parameter LightThrough//
                worldTile.LightTile.ApplyLight(true,m_LightForce);
                
                if (!worldTile.Tile.Walkable)
                    break;
            }
        }
    }
}
