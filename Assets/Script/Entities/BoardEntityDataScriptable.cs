using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoardEntity", menuName = "BoardEntity data", order = 0)]
public class BoardEntityDataScriptable : ScriptableObject
{
    public BoardEntityData m_EntityBaseData;
}

[System.Serializable]
public class BoardEntityData
{
    public EntityGroup m_EntityGroup = EntityGroup.Friendly;
    public EntityStats m_Stats = null;
    public SpellList m_SpellList = null;

    //Deep Copy => source : Good exemple with the difference between Deep and Shallow
    //"https://www.geeksforgeeks.org/shallow-copy-and-deep-copy-in-c-sharp/"
    //Struct idea for non complex classes ?
    //"https://stackoverflow.com/questions/5359318/how-to-clone-objects"
    public BoardEntityData(BoardEntityData data)
    {
        m_EntityGroup = data.m_EntityGroup;
        m_Stats = (EntityStats)data.m_Stats.Clone();
        m_SpellList = data.m_SpellList.Clone();
    }
}

[System.Serializable]
public class SpellList
{
    public List<SpellData> m_Spells = new List<SpellData>();

    public SpellList Clone()
    {
        List<SpellData> newSpells = new List<SpellData>();

        for (int i = 0; i < m_Spells.Count; i++)
        {
            newSpells.Add((SpellData)m_Spells[i].Clone());
        }
        
        return new SpellList(newSpells);
    }

    public SpellList(List<SpellData> spells)
    {
        m_Spells = spells;
    }
}