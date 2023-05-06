using UnityEngine;

public class BoardEntityMovement : MonoBehaviour
{
    private BoardEntity m_Entity = null;
    
    private Vector2Int m_ComputedInput = Vector2Int.zero;

    private float m_InputTiming = 0.05f;
    private float m_CurrentFecthInputTimer = -1;
    public void SetTargetEntity(BoardEntity target)
    {
        m_Entity = target;
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_ComputedInput.x = 1;
            TryLaunchInputFecth();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_ComputedInput.x = -1;
            TryLaunchInputFecth();
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_ComputedInput.y = 1;
            TryLaunchInputFecth();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_ComputedInput.y = -1;
            TryLaunchInputFecth();
        }

        if (m_CurrentFecthInputTimer >= 0)
        {
            m_CurrentFecthInputTimer -= Time.deltaTime;

            if (m_CurrentFecthInputTimer < 0)
            {
                TryMoveTo(m_ComputedInput);
                m_ComputedInput = Vector2Int.zero;
            }
        }
    }

    private void TryLaunchInputFecth()
    {
        if(m_CurrentFecthInputTimer < 0)
            m_CurrentFecthInputTimer = m_InputTiming;
    }
    
    public void TryMoveTo(Vector2Int pos)
    {
        if (!GameManager.Instance.CanPlay)
        {
            return;
        }
        
        Vector2Int targetPosition = m_Entity.EntityPosition + pos;

        if (MapData.Instance.IsWalkable(targetPosition))
        {
            m_Entity.MoveTo(targetPosition);
            GameManager.Instance.A_OnPlayerAction.Invoke(m_Entity);
        }
        else
        {
            SpellData autoAttack = m_Entity.GetSpellViaKey("AutoAttack").m_SpellData;
            
            if(autoAttack == null)
                return;

            BoardEntity entity = MapData.Instance.GetEntityAt(targetPosition, EntityHelper.GetInverseEntityGroup(m_Entity.EntityGroup));
            
            if(entity == null)
                return;
            
            Debug.Log("Auto Attack");
            SpellCastUtils.CastSpellAt(autoAttack as TriggerSpellData,targetPosition,m_Entity.EntityPosition);
            GameManager.Instance.A_OnPlayerAction.Invoke(m_Entity);
        }
    }
}