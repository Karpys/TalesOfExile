using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(fileName = "MonsterGeneration", menuName = "Monster/ClassicGeneration", order = 0)]
    public class MonsterGeneration : BaseMonsterGeneration
    {
        [SerializeField] private StaticWeightElementDraw<BoardEntity> m_WeightEnemies = null;
        [SerializeField] private float m_TargetEnemiesCount = 10;
        [SerializeField] private bool m_ShouldInheriteQuestModifier = true; 
    
        public override void Generate(List<Tile> allowedTiles)
        {
            float monsterSpawnChance = m_TargetEnemiesCount * 100 / allowedTiles.Count;
        
            foreach (Tile tile in allowedTiles)
            {
                float random = Random.Range(0f, 100f);

                if (random <= monsterSpawnChance)
                {
                    //Weight Ennemy
                    EntityHelper.SpawnEntityOnMap(tile.TilePosition,m_WeightEnemies.Draw(),new MapEnemyEntityBehaviour(),EntityGroup.Enemy,EntityGroup.None,m_ShouldInheriteQuestModifier);
                }
            }
        }
    }
}