﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MonsterGeneration", menuName = "Monster/ClassicGeneration", order = 0)]
public class MonsterGeneration : BaseMonsterGeneration
{
    [SerializeField] private StaticWeightElementDraw<BoardEntity> m_WeightEnemies = null;
    [SerializeField] private float m_TargetEnemiesCount = 10;
    
    public override void Generate(List<Tile> allowedTiles)
    {
        float monsterSpawnChance = m_TargetEnemiesCount * 100 / allowedTiles.Count;
        
        foreach (Tile tile in allowedTiles)
        {
            float random = Random.Range(0f, 100f);

            if (random <= monsterSpawnChance)
            {
                //Weight Ennemy
                EntityHelper.SpawnEntityOnMap(tile.TilePosition,m_WeightEnemies.Draw(),new MapEnemyEntityBehaviour(),EntityGroup.Enemy);
            }
        }
    }
}