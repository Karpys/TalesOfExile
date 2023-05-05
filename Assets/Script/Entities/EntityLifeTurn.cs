using System;
using UnityEngine;
public class EntityLifeTurn : MonoBehaviour
{
    [SerializeField] protected int m_BaseTurnCount = 10;
    private BoardEntity m_AttachedEntity = null;

    public void SetTurnCount(int turnCount)
    {
        m_BaseTurnCount = turnCount;
    }
    private void Awake()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
    }

    private void Start()
    {
        m_AttachedEntity.EntityEvent.OnBehave += ReduceLifeCounter;
    }

    private void OnDestroy()
    {
        m_AttachedEntity.EntityEvent.OnBehave -= ReduceLifeCounter;
    }

    protected virtual void ReduceLifeCounter()
    {
        m_BaseTurnCount -= 1;
        
        if(m_BaseTurnCount <= 0)
            m_AttachedEntity.ForceDeath();
    }
}