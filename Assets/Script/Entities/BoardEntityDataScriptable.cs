using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoardEntity", menuName = "BoardEntity data", order = 0)]
public class BoardEntityDataScriptable : ScriptableObject
{
    [Header("Base Entity Data")]
    public BoardEntityData m_EntityBaseData;
}

[System.Serializable]
public class BoardEntityData
{
    public EntityGroup m_EntityGroup = EntityGroup.Neutral;
    public EntityGroup m_TargetEntityGroup = EntityGroup.Neutral;
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
    public List<SpellInfo> m_Spells = new List<SpellInfo>();

    public SpellList Clone()
    {
        List<SpellInfo> newSpells = new List<SpellInfo>();

        for (int i = 0; i < m_Spells.Count; i++)
        {
            newSpells.Add(new SpellInfo(m_Spells[i].m_SpellData,m_Spells[i].m_SpellPriority));
        }
        
        return new SpellList(newSpells);
    }

    public SpellList(List<SpellInfo> spells)
    {
        m_Spells = spells;
    }
}

[System.Serializable]
public class SpellInfo
{
    public SpellData m_SpellData = null;
    public int m_SpellPriority = 0;
    
    public SpellInfo(SpellData spellData, int priority)
    {
        m_SpellData = (SpellData)spellData.Clone();
        m_SpellPriority = priority;
    }
}