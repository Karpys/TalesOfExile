using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Utils;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class PLayerAutoPlayEntity : BaseEntityIA
    {
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
}