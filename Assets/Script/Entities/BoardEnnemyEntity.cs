using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class BoardEnnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           
        }
    }

    public override void EntityAction()
    {
        List<Tile> path = PathFinding.FindTilePath(EntityPosition, m_TargetMap.GetControlledEntityPosition(),false);

        //Debug//
        //List<Vector2Int> paths = new List<Vector2Int>();
        //foreach (Tile tile in path)
        //{
        //    paths.Add(new Vector2Int(tile.XPos,tile.YPos));
        //}
        //HighlightTilesManager.Instance.HighlightTiles(paths);
        
        //Check if the path to the player is lower than the range//
        if (path.Count == 1)
        {
            //TEMP !!//
            TriggerSpellData triggerSpellData = m_EntityData.m_SpellList.m_Spells[0] as TriggerSpellData;
            
            if(triggerSpellData == null)
                return;
            
            CastSpellAt(triggerSpellData,m_TargetMap.GetControlledEntityPosition());
        }

        //Movement Action//
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
            Vector2Int targetPos = TileHelper.GetOppositePosition(EntityPosition, path[0].TilePosition);
            if (MapData.Instance.IsWalkable(targetPos))
                MoveTo(targetPos);
        }
    }

    protected override void Movement()
    {
        transform.DoKill();
        transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
    }
    
    //Damage Related

    protected override void TriggerDeath()
    {
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
    }
}