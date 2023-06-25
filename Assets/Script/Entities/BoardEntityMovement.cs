using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class BoardEntityMovement : MonoBehaviour
    {
        [SerializeField] private SpellInterpretor m_Interpretor = null;
    
        private BoardEntity m_Entity = null;
    
        private Vector2Int m_ComputedInput = Vector2Int.zero;

        private float m_InputTiming = 0.05f;
        private float m_CurrentFecthInputTimer = -1;

        private bool m_OnRepeat = false;
        public void SetTargetEntity(BoardEntity target)
        {
            m_Entity = target;
        }
    
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                WaitTurn();
                return;
            }
            
            if (InputManager.Instance.IsMovementKeyHold)
            {
                HoldConfiguration();   
            }
            else
            {
                TouchConfiguration();
            }

            if (m_CurrentFecthInputTimer >= 0)
            {
                m_CurrentFecthInputTimer -= Time.deltaTime;

                if (m_CurrentFecthInputTimer < 0)
                {
                    TryMoveTo(m_ComputedInput);
                    m_ComputedInput = Vector2Int.zero;
                    m_OnRepeat = true;
                }
            }
        }

        private void WaitTurn()
        {
            m_Entity.EntityEvent.OnBehave?.Invoke();
            GameManager.Instance.A_OnPlayerAction.Invoke(m_Entity);
        }
        
        private void TouchConfiguration()
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
        }
        private void HoldConfiguration()
        {
            if (Input.GetKey(KeyCode.D))
            {
                m_ComputedInput.x = 1;
                TryLaunchInputFecth();
            }
        
            if (Input.GetKey(KeyCode.Q))
            {
                m_ComputedInput.x = -1;
                TryLaunchInputFecth();
            }
        
            if (Input.GetKey(KeyCode.Z))
            {
                m_ComputedInput.y = 1;
                TryLaunchInputFecth();
            }
        
            if (Input.GetKey(KeyCode.S))
            {
                m_ComputedInput.y = -1;
                TryLaunchInputFecth();
            }
        }

        private void TryLaunchInputFecth()
        {
            if(m_CurrentFecthInputTimer < 0)
                m_CurrentFecthInputTimer = m_InputTiming;
        }
    
        public void TryMoveTo(Vector2Int pos)
        {
            if (!GameManager.Instance.CanPlay || m_Entity.EntityStats.RootLockCount > 0)
            {
                return;
            }
        
            Vector2Int targetPosition = m_Entity.EntityPosition + pos;

            if (MapData.Instance.IsWalkable(targetPosition))
            {
                m_Entity.MoveTo(targetPosition);
                m_Interpretor.OnMovementResetSpellQueue();
                m_Entity.EntityEvent.OnBehave?.Invoke();
                GameManager.Instance.A_OnPlayerAction.Invoke(m_Entity);
            }
            else
            {
                TriggerSpellData autoAttack = m_Entity.GetUsableViaKey("AutoAttack");
            
                if(autoAttack == null)
                    return;

                BoardEntity entity = MapData.Instance.GetEntityAt(targetPosition, EntityHelper.GetInverseEntityGroup(m_Entity.EntityGroup));
            
                if(entity == null)
                    return;
            
                Debug.Log("Auto Attack");
                //Todo : Spell Restriction Check//
                SpellCastUtils.CastSpellAt(autoAttack,targetPosition,m_Entity.EntityPosition);
                m_Interpretor.OnMovementResetSpellQueue();
                m_Entity.EntityEvent.OnBehave?.Invoke();
                GameManager.Instance.A_OnPlayerAction.Invoke(m_Entity);
            }
        }
    }
}