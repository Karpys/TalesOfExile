using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlightSpawner:WorldTile
{
    [SerializeField] private BoardEntity m_BlightMonster = null;
    [SerializeField] private Vector2Int m_Clock = new Vector2Int(2,5);
    [SerializeField] private int m_BlightMonsterCount = 10;

    private int m_CurrentClock = 0;
    private bool m_IsActive = false;
    private List<Tile> m_BranchPath = null;
    private void Start()
    {
        GameManager.Instance.A_OnEndTurn += TrySpawnBlightMonster;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= TrySpawnBlightMonster;
    }

    public void ActiveBlight(bool active)
    {
        m_IsActive = active;
    }
    public void SetPath(List<Tile> branchPath)
    {
        m_BranchPath = branchPath;
        m_BranchPath.Reverse();
    }
    private void TrySpawnBlightMonster()
    {
        if(!m_IsActive || m_BlightMonsterCount <= 0)
            return;

        m_CurrentClock -= 1;
        
        if (m_CurrentClock < 0)
        {
            BoardEntity entity = EntityHelper.SpawnEntityOnMap(m_BlightMonster, Tile.XPos, Tile.YPos, MapData.Instance);
            entity.SetEntityBehaviour(new BlightBehaviour(entity,this));
            m_CurrentClock = Random.Range(m_Clock.x, m_Clock.y);
            m_BlightMonsterCount -= 1;
        }

    }
    
    public Tile GetNextBranchPath(int id)
    {
        if (id >= m_BranchPath.Count - 1)
            return null;
        return m_BranchPath[id + 1];
    }
}
