using System;
using System.Collections;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.SkillTree;
using KarpysDev.Script.UI;
using KarpysDev.Script.UI.ItemContainer;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        [Header("UI reference")]
        [SerializeField] private Canvas_Skills m_CanvasSkills = null;
        [SerializeField] private CanvasSkillLearned m_CanvasSkillLearned = null;
        [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
        [SerializeField] private SkillTreeController m_SkillTreeController = null;
        //Widget//
        [SerializeField] private BoardEntityMovement m_EntityInputMovement = null;
        [SerializeField] private bool m_RemoveDelay = false;

        private PlayerBoardEntity m_PlayerEntity = null;
        //Controlled Entity//
        private BoardEntity m_ControlledEntity = null;
        public BoardEntity ControlledEntity => m_ControlledEntity;
        public PlayerBoardEntity PlayerEntity => m_PlayerEntity;
    
        //Ennemies//
        private List<BoardEntity> m_EntitiesOnBoard = new List<BoardEntity>();
        private List<BoardEntity> m_EnemiesOnBoard = new List<BoardEntity>();
        private List<BoardEntity> m_FriendlyOnBoard = new List<BoardEntity>();
        private List<BoardEntity> m_ActiveEnemiesOnBoard = new List<BoardEntity>();
        public List<BoardEntity> EntitiesOnBoard => m_EntitiesOnBoard;
        public List<BoardEntity> EnemiesOnBoard => m_EnemiesOnBoard;
        public List<BoardEntity> FriendlyOnBoard => m_FriendlyOnBoard;
        public List<BoardEntity> ActiveEnemiesOnBoard => m_ActiveEnemiesOnBoard;
    
        //Action Queue//
        private bool m_CanPlay = true;
        private bool m_InCallBackExecution = false;
        [SerializeField] private float m_FriendlyBaseWaitTime = 0f;
        [HideInInspector] public float FriendlyWaitTime = 0f;
        [HideInInspector] public float EnnemiesWaitTime = 0f;

        public bool CanPlay => m_CanPlay;
        //Action//
        public Action<BoardEntity> A_OnPlayerAction;
        public Action A_OnPreEndTurn;
        public Action A_OnEndTurn;
        public Action A_OnEndPlayerTurn;
        public Action A_OnEndEnemyTurn;
        public Action A_OnFriendlyBehave;
        public Action A_OnEnemyBehave;
    
        public Action<BoardEntity,BoardEntity> A_OnControlledEntityChange;
        private Action A_CallBackEndAction;

        //Lock//
        private int m_LockCount = 0;
    
        //Test Auto Play//
        private bool m_AutoPlay = false;
        private void Awake()
        {
            A_OnPlayerAction += LaunchActionQueue;
        }
    
        //Debug//
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                m_AutoPlay = !m_AutoPlay;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                m_RemoveDelay = !m_RemoveDelay;
            }

            if (m_AutoPlay && m_CanPlay)
            {
                LaunchActionQueue(m_ControlledEntity);
            }
        }
        //INTIALIZATION ENTITY//
    
        //Set the player value//
        public void RegisterPlayer(PlayerBoardEntity player)
        {
            if (m_PlayerEntity == player)
            {
                Debug.LogError("Register twice same player entity ?");
            }
            
            m_PlayerEntity = player;
            m_PlayerInventoryUI.SetPlayerInventory(player.PlayerInventory);
            m_SkillTreeController.Initialize(player);
            m_CanvasSkillLearned.Initialize(player);
        }

        public void SetControlledEntity(BoardEntity entity)
        {
            BoardEntity oldControlledEntity = m_ControlledEntity;
        
            m_ControlledEntity = entity;
            m_EntityInputMovement.SetTargetEntity(entity);
            RefreshTargetEntitySkills();
        
            A_OnControlledEntityChange?.Invoke(oldControlledEntity,entity);
        }

        //Add a an entity class to the list of all entity//
        public void RegisterActiveEnemy(BoardEntity entity)
        {
            m_ActiveEnemiesOnBoard.Add(entity);
        }
    
        public void UnRegisterActiveEnemy(BoardEntity entity)
        {
            m_ActiveEnemiesOnBoard.Remove(entity);
        }
        public void RegisterEntity(BoardEntity entity)
        {
            m_EntitiesOnBoard.Add(entity);
        
            //Sort Entity
            if(entity.EntityGroup == EntityGroup.Friendly)
                m_FriendlyOnBoard.Add(entity);
            else if(entity.EntityGroup == EntityGroup.Enemy)
                m_EnemiesOnBoard.Add(entity);
        }

        public void UnRegisterEntity(BoardEntity entity)
        {
            m_EntitiesOnBoard.Remove(entity);
        
            if (entity.EntityGroup == EntityGroup.Enemy)
                m_EnemiesOnBoard.Remove(entity);
            else if(entity.EntityGroup == EntityGroup.Friendly)
                m_FriendlyOnBoard.Remove(entity);
        }

        public List<BoardEntity> GetEntityViaGroup(EntityGroup group)
        {
            if (group == EntityGroup.Enemy)
                return new List<BoardEntity>(m_ActiveEnemiesOnBoard);
            else if (group == EntityGroup.Friendly)
                return new List<BoardEntity>(m_FriendlyOnBoard);
            return null;
        }
    
        //UI Manager//
        public void RefreshTargetEntitySkills()
        {
            if(m_ControlledEntity != null)
                m_CanvasSkills.RefreshTargetSkills(m_ControlledEntity);
        }
        //OnPlayer Action//

        private void LaunchActionQueue(BoardEntity inputEntity)
        {
            if (m_RemoveDelay)
            {
                TriggerAllFriendlyActions();
                A_OnEndPlayerTurn?.Invoke();
                m_InCallBackExecution = true;
                A_CallBackEndAction?.Invoke();
                m_InCallBackExecution = false;
                TriggerAllEnemyAction();
                A_OnEndEnemyTurn?.Invoke();
                m_InCallBackExecution = true;
                A_CallBackEndAction?.Invoke();
                m_InCallBackExecution = false;
                ClearCallBackAction();
                A_OnPreEndTurn?.Invoke();
                A_OnEndTurn?.Invoke();
                return;
            }
        
            AddLock();
            StartCoroutine(I_ActionQueue());
        
            IEnumerator I_ActionQueue()
            {
                TriggerAllFriendlyActions();
                yield return new WaitForSeconds(FriendlyWaitTime);
                FriendlyWaitTime = 0;
                A_OnEndPlayerTurn?.Invoke();

                m_InCallBackExecution = true;
                A_CallBackEndAction?.Invoke();
                m_InCallBackExecution = false;
                yield return new WaitForSeconds(FriendlyWaitTime);
                ClearCallBackAction();
            
                TriggerAllEnemyAction();
                yield return new WaitForSeconds(EnnemiesWaitTime);
            
                EnnemiesWaitTime = 0;
                A_OnEndEnemyTurn?.Invoke();
            
                m_InCallBackExecution = true;
                A_CallBackEndAction?.Invoke();
                m_InCallBackExecution = false;
                yield return new WaitForSeconds(EnnemiesWaitTime);
                ClearCallBackAction();
                A_OnPreEndTurn?.Invoke();
                A_OnEndTurn?.Invoke();
                ResetActionQueue();
            }
        }

        private void ClearCallBackAction()
        {
            A_CallBackEndAction = null;
        }

        public bool AddCallBackAction(Action action, bool force = false)
        {
            if(m_InCallBackExecution && force == false)
                return false;
        
            A_CallBackEndAction += action;
            return true;
        }

        private void ResetActionQueue()
        {
            ReleaseLock();
            FriendlyWaitTime = m_FriendlyBaseWaitTime;
            EnnemiesWaitTime = 0;
        }

        public void AddLock()
        {
            m_LockCount += 1;
            if (m_LockCount > 0)
                m_CanPlay = false;
        }

        public void ReleaseLock()
        {
            m_LockCount -= 1;
            if (m_LockCount <= 0)
                m_CanPlay = true;
        }
        //Enemy Action//
        private void TriggerAllFriendlyActions()
        {
            List<BoardEntity> friendly = new List<BoardEntity>(m_FriendlyOnBoard);
        
            for (int i = 0; i < friendly.Count; i++)
            {
                BoardEntity entity = friendly[i];

                if (!entity)
                {
                    Debug.LogError("Entity dead Continue");   
                    continue;
                }
            
                if (entity == m_ControlledEntity && !m_AutoPlay)
                {
                    entity.ReduceAllCooldown();
                    continue;
                }
            
                friendly[i].EntityAction();
            }
        
            A_OnFriendlyBehave?.Invoke();
        }
        private void TriggerAllEnemyAction()
        {
            List<BoardEntity> enemies = new List<BoardEntity>(m_EnemiesOnBoard);
        
            for (int i = 0; i < enemies.Count; i++)
            {
                BoardEntity entity = enemies[i];
            
                if(!entity)
                    continue;
            
                entity.EntityAction();
            }
        
            A_OnEnemyBehave?.Invoke();
        }
    }
}
