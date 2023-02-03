using System.Collections.Generic;
using Script.PathFinding;
using Script.Utils;
using TweenCustom;
using UnityEngine;

public class BoardEnnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterEnemy(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           
        }
    }

    public virtual void EnemmyAction()
    {
        List<Tile> path = PathFinding.FindTilePath(EntityPosition, m_TargetMap.GetPlayerPosition(),true);

        //Debug//
        List<Vector2Int> paths = new List<Vector2Int>();
        foreach (Tile tile in path)
        {
            paths.Add(new Vector2Int(tile.XPos,tile.YPos));
        }
        HighlightTilesManager.Instance.HighlightTiles(paths);
        
        //Check if the path to the player is lower than the range//
        if (path.Count > m_Range)
        {
            //Move toward player//
            if (MapData.Instance.IsWalkable(path[0].TilePosition))
            {
                MoveTo(path[0].TilePosition);
            }
        }
        else if(path.Count < m_Range)
        {
            //Run Away from player if too close//
            Vector2Int targetPos = BoardUtils.GetOppositePosition(EntityPosition, path[0].TilePosition);
            if (MapData.Instance.IsWalkable(targetPos))
                MoveTo(targetPos);
        }
    }

    protected override void Movement()
    {
        transform.DoKill();
        transform.DoMove(0.1f, m_TargetMap.GetTilePosition(m_XPosition, m_YPosition));
    }
}