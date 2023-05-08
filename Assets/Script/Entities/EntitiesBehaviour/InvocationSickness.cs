using System;
using UnityEngine;

public class InvocationSickness : MonoBehaviour
{
    [SerializeField] protected int m_TurnWaitBehave = 1;
    
    private BoardEntity m_AttachedEntity = null;
    private void Awake()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        m_AttachedEntity.SetBehaveState(false);

        GameManager.Instance.A_OnEndTurn += EntityCanBehave;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= EntityCanBehave;
    }

    protected virtual void EntityCanBehave()
    {
        if (m_TurnWaitBehave <= 0)
        {
            RemoveSickness();
        }
        
        m_TurnWaitBehave -= 1;

        if (m_TurnWaitBehave <= 0)
        {
            m_AttachedEntity.SetBehaveState(true);
        }
    }

    protected virtual void RemoveSickness()
    {
        Destroy(this);
    }
}