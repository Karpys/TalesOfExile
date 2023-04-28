using System.Collections.Generic;
using UnityEngine;

public class MapEnemyEntityBehaviour : BaseEntityIA
{
    private bool m_IsActive = false;

    private const int X_ACTIVE_TOLERANCE = 19;
    private const int Y_ACTIVE_TOLERANCE = 11;
    
    protected override void InitializeEntityBehaviour()
    {
        SelfBuffCount();
        ComputeSpellPriority();
    }
    
    private void CheckForActive()
    {
        Vector2Int entityPos = GameManager.Instance.ControlledEntity.EntityPosition;

        int XDiff = Mathf.Abs(entityPos.x - m_AttachedEntity.EntityPosition.x);
        int YDiff = Mathf.Abs(entityPos.y - m_AttachedEntity.EntityPosition.y);

        if (XDiff < X_ACTIVE_TOLERANCE && YDiff < Y_ACTIVE_TOLERANCE)
        {
            GameManager.Instance.RegisterActiveEnemy(m_AttachedEntity);
            m_IsActive = true;
        }
    }

    public override void Behave()
    {
        if (!m_IsActive)
        {
            CheckForActive();

            if (!m_IsActive)
                return;
        }
        
        base.Behave();
    }
}