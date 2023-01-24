using UnityEngine;

public class BoardEnnemyEntity : BoardEntity
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           Tile targetTile = m_TargetMap.FindClosestTile(EntityPosition, m_TargetMap.GetPlayerPosition());
           MoveTo(targetTile.XPos,targetTile.YPos);
        }
    }
}