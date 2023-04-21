using System;
using UnityEngine;
public class EntityLifeTurn : MonoBehaviour
{
    private int m_TurnCount = 10;
    private BoardEntity m_AttachedEntity = null;

    public void SetTurnCount(int turnCount)
    {
        m_TurnCount = turnCount;
    }
    private void Awake()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        m_AttachedEntity.EntityEvent.OnBehave += ReduceLifeCounter;
    }

    private void OnDestroy()
    {
        m_AttachedEntity.EntityEvent.OnBehave -= ReduceLifeCounter;
    }

    private void ReduceLifeCounter()
    {
        m_TurnCount -= 1;
        
        if(m_TurnCount <= 0)
            m_AttachedEntity.ForceDeath();
    }
}
