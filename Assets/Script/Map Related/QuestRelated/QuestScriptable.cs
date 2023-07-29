using System;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest", order = 0)]
    public class QuestScriptable : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string m_QuestName = String.Empty;
        [SerializeField] private Sprite m_QuestPortalIcon = null;
        [Header("Modifier")]
        [SerializeField] private QuestModifierScriptable m_BonusMapModifier = null;
        [SerializeField] private QuestModifierScriptable m_MalusMapModifier = null;
        [Header("Map")]
        [SerializeField] private MapGroup m_MapGroup = null;
        [Header("Loot")]
        [SerializeField] private float m_BaseGoldAmount = 0;
        [SerializeField] private float m_BaseExpAmount = 0;

        public string QuestName => m_QuestName;
        public float BaseGoldAmount => m_BaseGoldAmount;
        public float BaseExpAmount => m_BaseExpAmount;
        public QuestModifierScriptable BonusMapModifier => m_BonusMapModifier;
        public QuestModifierScriptable MalusMapModifier => m_MalusMapModifier;
        public MapGroup MapGroup => m_MapGroup;
        public Sprite QuestPortalIcon => m_QuestPortalIcon;
    }

    public enum QuestModifierType
    {
        None = 0,
        //1 => 25 Malus Modifier
        AddPercentLife = 1,
        AddPercentPhysicalResistance = 2,
        //26 => ++ Bonus Modifier//
        LootPercentage = 26,
        AddPlayerRegeneration = 27,
    }
    
    public enum QuestDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Extreme =3,
    }
}