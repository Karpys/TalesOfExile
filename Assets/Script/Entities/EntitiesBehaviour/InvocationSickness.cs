using System;
using UnityEngine;

public class InvocationSickness : MonoBehaviour
{
    [SerializeField] private int m_TurnWaitBehave = 1;
    
    private BoardEntity m_AttachedEntity = null;
    private void Awake()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        m_AttachedEntity.SetBehaveState(false);

        GameManager.Instance.A_OnEndTurn += EntityCanBehave;
    }

    private void OnDestroy()
    {
        GameManager.Instance.A_OnEndTurn -= EntityCanBehave;
    }

    private void EntityCanBehave()
    {
        m_TurnWaitBehave -= 1;

        if (m_TurnWaitBehave <= 0)
        {
            m_AttachedEntity.SetBehaveState(true);
            Destroy(this);
        }
    }
}
