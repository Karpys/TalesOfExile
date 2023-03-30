using System;
using System.Collections.Generic;
using UnityEngine;

public class BlightSpawner:WorldTile
{
    [SerializeField] private BoardEntity m_BlightMonster = null;

    private List<Tile> m_BranchPath = null;
    private void Start()
    {
        SpawnBlightMonster();
        //GameManager.Instance.A_OnEndTurn += SpawnBlightMonster;
    }

    public void SetPath(List<Tile> branchPath)
    {
        m_BranchPath = branchPath;
        m_BranchPath.Reverse();
    }

    public Tile GetNextBranchPath(int id)
    {
        if (id >= m_BranchPath.Count - 1)
            return null;
        return m_BranchPath[id + 1];
    }
    private void SpawnBlightMonster()
    {
        BoardEntity entity = EntityHelper.SpawnEntityOnMap(m_BlightMonster, Tile.XPos, Tile.YPos, MapData.Instance);
        entity.SetEntityBehaviour(new BlightBehaviour(entity,this));
    }
}
