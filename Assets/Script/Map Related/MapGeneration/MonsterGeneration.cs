using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterGeneration", menuName = "Monster/ClassicGeneration", order = 0)]
public class MonsterGeneration : ScriptableObject
{
    [SerializeField] private WeightEnemyDraw m_WeightEnemies = null;
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
                BoardEntity enemy = Instantiate(m_WeightEnemies.Draw(),map.transform);
                enemy.Place(tile.XPos,tile.YPos,map);
            }
        }
    }
}

[System.Serializable]
public class WeightEnemyDraw
{
    [SerializeField] private WeightEnemy[] m_Enemies = null;
    public BoardEntity Draw()
    {
        float totalWeight = 0;

        foreach (WeightEnemy enemy in m_Enemies)
        {
            totalWeight += enemy.Weight;
        }

        float drawWeight = Random.Range(0f, totalWeight);
        int entityId = 0;
        float currentWeight = 0;
        
        while (entityId < m_Enemies.Length - 1)
        {
            currentWeight += m_Enemies[entityId].Weight;
            if (drawWeight < currentWeight)
            {
                break;
            }
            else
            {
                entityId += 1;
            }
        }
        
        return m_Enemies[entityId].Entity;
    }
}

[System.Serializable]
public class WeightEnemy
{
    [SerializeField] [Range(0,100)] private float m_Weight = 50;
    [SerializeField] private BoardEntity m_Entity = null;

    public float Weight => m_Weight;
    public BoardEntity Entity => m_Entity;
}
