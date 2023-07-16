using System.Collections.Generic;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(fileName = "MonsterGeneration", menuName = "Monster/EntitySpawnGeneration", order = 0)]
    public class EntitySpawnGeneration : BaseMonsterGeneration
    {
        [SerializeField] private EntitySpawn[] m_EntitySpawns = null;
    
        public override void Generate(List<Tile> allowedTiles)
        {
            EntityHelper.SpawnViaEntitySpawn(m_EntitySpawns);
        }
    }

    public abstract class BaseMonsterGeneration : ScriptableObject
    {
        public abstract void Generate(List<Tile> allowedTiles);
    }
}