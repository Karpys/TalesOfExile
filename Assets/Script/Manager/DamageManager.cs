using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageManager : SingletonMonoBehavior<DamageManager>
{
    public void TryDamageEnnemy(Vector2Int pos, BoardEntity damageFrom,out BoardEntity damageTo)//Add DamageClass
    {
        damageTo = GetEntityAt(pos);

        if (damageTo == null)
        {
            Debug.Log("No entity found at :" + pos +" continue");
            return;
        }

        DamageStep(damageTo,damageFrom);//Add DamageClass
    }

    private void DamageStep(BoardEntity damageTo,BoardEntity damageFrom)
    {
        //Do Something//
        //Do damage//
        damageTo.MoveTo(10,10);
    }

    public BoardEntity GetEntityAt(Vector2Int entityPos)
    {
        List<BoardEnnemyEntity> boardEntities = GameManager.Instance.Ennemies;
        
        for (int i = 0; i < boardEntities.Count; i++)
        {
            if (boardEntities[i].EntityPosition == entityPos)
                return boardEntities[i];
        }
        return null;
    }
}
