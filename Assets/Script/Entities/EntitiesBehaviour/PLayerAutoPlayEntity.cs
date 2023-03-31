using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PLayerAutoPlayEntity : BaseEntityIA
{
    public PLayerAutoPlayEntity(BoardEntity entity) : base(entity)
    {
    }

    protected override void SetTarget()
    {
        List<BoardEntity> entities = m_AttachedEntity.EntityGroup == EntityGroup.Friendly ? GameManager.Instance.EnemiesOnBoard : GameManager.Instance.FriendlyOnBoard;

        if (entities.Count == 0)
        {
            m_Target = null;
            return;
        }

        m_Target = entities.OrderBy(e => DistanceUtils.GetSquareDistance(m_AttachedEntity.EntityPosition, e.EntityPosition)).First();
    }
}