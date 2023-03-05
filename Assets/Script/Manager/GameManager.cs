using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    //Widget//
    [SerializeField] private BoardEntityMovement m_EntityInputMovement = null;
    [SerializeField] private bool m_RemoveDelay = false;

    private PlayerBoardEntity m_PlayerEntity = null;
    //Controlled Entity//
    private BoardEntity m_ControlledEntity = null;
    [HideInInspector] public BoardEntity ControlledEntity => m_ControlledEntity;
    [HideInInspector] public PlayerBoardEntity PlayerEntity => m_PlayerEntity;
    
    //Ennemies//
    private List<BoardEntity> m_EntitiesOnBoard = new List<BoardEntity>();
    private List<BoardEntity> m_EnnemiesOnBoard = new List<BoardEntity>();
    private List<BoardEntity> m_FriendlyOnBoard = new List<BoardEntity>();
    public List<BoardEntity> EntitiesOnBoard => m_EntitiesOnBoard;
    public List<BoardEntity> EnnemiesOnBoard => m_EnnemiesOnBoard;
    public List<BoardEntity> FriendlyOnBoard => m_FriendlyOnBoard;
    
    //Action Queue//
    private bool m_CanPlay = true;
    [SerializeField] private float m_FriendlyBaseWaitTime = 0f;
    [HideInInspector] public float FriendlyWaitTime = 0f;
    [HideInInspector] public float EnnemiesWaitTime = 0f;

    public bool CanPlay => m_CanPlay;
    //Action//
    public Action<BoardEntity> A_OnPlayerAction;
    public Action<BoardEntity,BoardEntity> A_OnControlledEntityChange;

    private void Awake()
    {
        A_OnPlayerAction += LaunchActionQueue;
    }
    
    //Debug//
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerAllEnnemyAction();
        }
    }
    //INTIALIZATION ENTITY//
    
    //Set the player value//
    public void RegisterPlayer(PlayerBoardEntity player)
    {
        m_PlayerEntity = player;
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
    public void RegisterEntity(BoardEntity entity)
    {
        m_EntitiesOnBoard.Add(entity);
        
        //Sort Entity
        if(entity.EntityGroup == EntityGroup.Friendly)
            m_FriendlyOnBoard.Add(entity);
        else if(entity.EntityGroup == EntityGroup.Ennemy)
            m_EnnemiesOnBoard.Add(entity);
    }

    public void UnRegisterEntity(BoardEntity entity)
    {
        m_EntitiesOnBoard.Remove(entity);
        
        if (entity.EntityGroup == EntityGroup.Ennemy)
            m_EnnemiesOnBoard.Remove(entity);
        else if(entity.EntityGroup == EntityGroup.Friendly)
            m_FriendlyOnBoard.Remove(entity);
    }
    
    //UI Manager//
    public void RefreshTargetEntitySkills()
    {
        Canvas_Skills.Instance.SetTargetSkills(m_ControlledEntity);
    }
    //OnPlayer Action//

    private void LaunchActionQueue(BoardEntity inputEntity)
    {
        if (m_RemoveDelay)
        {
            TriggerAllFriendlyActions();
            TriggerAllEnnemyAction();
            return;
        }
        
        m_CanPlay = false;
        StartCoroutine(I_ActionQueue());
        
        IEnumerator I_ActionQueue()
        {
            TriggerAllFriendlyActions();
            yield return new WaitForSeconds(FriendlyWaitTime);
            TriggerAllEnnemyAction();
            yield return new WaitForSeconds(EnnemiesWaitTime);
            ResetActionQueue();
        }
    }

    private void ResetActionQueue()
    {
        m_CanPlay = true;
        FriendlyWaitTime = m_FriendlyBaseWaitTime;
        EnnemiesWaitTime = 0;
    }

    //Enemy Action//
    private void TriggerAllFriendlyActions()
    {
        for (int i = 0; i < m_FriendlyOnBoard.Count; i++)
        {
            m_FriendlyOnBoard[i].EntityAction();
        }
    }
    private void TriggerAllEnnemyAction()
    {
        for (int i = 0; i < m_EnnemiesOnBoard.Count; i++)
        {
            m_EnnemiesOnBoard[i].EntityAction();
        }
    }
}
