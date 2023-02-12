using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    //Widget//
    [SerializeField] private BoardEntityMovement m_EntityInputMovement = null;

    private PlayerBoardEntity m_PlayerEntity = null;
    //Controlled Entity//
    private BoardEntity m_ControlledEntity = null;
    public BoardEntity ControlledEntity => m_ControlledEntity;
    public PlayerBoardEntity PlayerEntity => m_PlayerEntity;
    
    //Ennemies//
    private List<BoardEntity> m_EntitiesOnBoard = new List<BoardEntity>();
    private List<BoardEntity> m_EnnemiesOnBoard = new List<BoardEntity>();
    private List<BoardEntity> m_FriendlyOnBoard = new List<BoardEntity>();
    public List<BoardEntity> EntitiesOnBoard => m_EntitiesOnBoard;
    public List<BoardEntity> EnnemiesOnBoard => m_EnnemiesOnBoard;
    public List<BoardEntity> FriendlyOnBoard => m_FriendlyOnBoard;
    
    //Action//
    public Action<BoardEntity> A_OnPlayerAction;
    public Action<BoardEntity,BoardEntity> A_OnControlledEntityChange;

    private void Awake()
    {
        A_OnPlayerAction += OnPlayerAction;
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
        SetTargetEntitySkills(entity);
        
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
    public void SetTargetEntitySkills(BoardEntity entity)
    {
        Canvas_Skills.Instance.SetTargetSkills(entity);
    }
    //OnPlayer Action//

    private void OnPlayerAction(BoardEntity inputEntity)
    {
        TriggerAllEnnemyAction();
    }
    
    //Enemy Action//
    private void TriggerAllEnnemyAction()
    {
        for (int i = 0; i < m_EntitiesOnBoard.Count; i++)
        {
            m_EntitiesOnBoard[i].EntityAction();
        }
    }
}
