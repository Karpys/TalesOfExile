using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    //Widget//
    [SerializeField] private BoardEntityMovement m_EntityInputMovement = null;
    //Player//
    private PlayerBoardEntity m_Player = null;
    public PlayerBoardEntity Player => m_Player;
    
    public Action<BoardEntity> A_OnPlayerAction; 
    //Ennemies//
    private List<BoardEnnemyEntity> m_Ennemies = new List<BoardEnnemyEntity>();


    private void Awake()
    {
        A_OnPlayerAction += OnPlayerAction;
    }
    //INTIALIZATION ENTITY//
    
    //Set the player value//
    public void RegisterPlayer(PlayerBoardEntity player)
    {
        m_Player = player;
        m_EntityInputMovement.SetTargetEntity(m_Player);
        SetTargetEntitySkills(player);
    }

    //Add a ennemy base class to the list of all ennemies//
    public void RegisterEnemy(BoardEnnemyEntity ennemy)
    {
        m_Ennemies.Add(ennemy);
    }
    
    //UI Manager//
    public void SetTargetEntitySkills(BoardEntity entity)
    {
        Canvas_Skills.Instance.SetTargetSkills(entity);
    }
    //OnPlayer Action//

    private void OnPlayerAction(BoardEntity inputEntity)
    {
        for (int i = 0; i < m_Ennemies.Count; i++)
        {
            m_Ennemies[i].EnemmyAction();
        }
    }
}
