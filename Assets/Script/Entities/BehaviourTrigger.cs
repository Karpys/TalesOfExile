using System;
using UnityEngine;

public class BehaviourTrigger : MonoBehaviour
{
    private int m_BehaveCount = 0;
    private BehaveTiming m_BehaveTiming = BehaveTiming.Friendly;
    private IBehave m_Behave = null;
    
    public void InitBehaviourTrigger(IBehave behave,BehaveTiming timing,int behaveCount)
    {
        m_BehaveCount = behaveCount;
        m_Behave = behave;
        m_BehaveTiming = timing;

        switch (m_BehaveTiming)
        {
            case BehaveTiming.Friendly:
                GameManager.Instance.A_OnFriendlyBehave += TriggerBehave;
                break;
            case BehaveTiming.Enemy:
                GameManager.Instance.A_OnEnemyBehave += TriggerBehave;
                break;
            case BehaveTiming.EndTurn:
                GameManager.Instance.A_OnPreEndTurn += TriggerBehave;
                break;
            default:
                GameManager.Instance.A_OnPreEndTurn += TriggerBehave;
                break;
        }
    }

    public void OnDestroy()
    {
        if(!GameManager.Instance)
            return;
        
        switch (m_BehaveTiming)
        {
            case BehaveTiming.Friendly:
                GameManager.Instance.A_OnFriendlyBehave -= TriggerBehave;
                break;
            case BehaveTiming.Enemy:
                GameManager.Instance.A_OnEnemyBehave -= TriggerBehave;
                break;
            case BehaveTiming.EndTurn:
                GameManager.Instance.A_OnPreEndTurn -= TriggerBehave;
                break;
        }
    }

    private void TriggerBehave()
    {
        m_Behave.Behave();

        m_BehaveCount--;

        if (m_BehaveCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum BehaveTiming
{
    Friendly,
    Enemy,
    EndTurn,
    SameAsSource,
}

public interface IBehave
{
    void Behave();
}
