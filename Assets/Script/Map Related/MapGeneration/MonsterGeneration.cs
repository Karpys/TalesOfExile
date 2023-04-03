using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MonsterGeneration", menuName = "Monster/ClassicGeneration", order = 0)]
public class MonsterGeneration : ScriptableObject
{
    [SerializeField] private WeightElementDraw<BoardEntity> m_WeightEnemies = null;
    [SerializeField] private float m_TargetEnemiesCount = 10;

    public void GenerateEnemies(List<Tile> targetTiles,MapData map)
    {
        float monsterSpawnChance = m_TargetEnemiesCount * 100 / targetTiles.Count;
        
        foreach (Tile tile in targetTiles)
        {
            float random = Random.Range(0f, 100f);

            if (random <= monsterSpawnChance)
            {
                //Weight Ennemy
                EntityHelper.SpawnEntityOnMap(m_WeightEnemies.Draw(), tile.XPos, tile.YPos, map);
            }
        }
    }
}