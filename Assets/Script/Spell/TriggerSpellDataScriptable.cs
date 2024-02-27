using System;
using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Trigger/A_TriggerSpell", order = 0)]
    public class TriggerSpellDataScriptable : SpellDataScriptable
    {
        [Header("Trigger Spell Data")]
        //FIX PARAMETERS//
        [Header("Spell UI")]
        [SerializeField] private Sprite m_SpellIconBorder = null;
        [SerializeField] private Sprite m_SpellIcon = null;

        [Header("Spell Trigger Options")]
        //Add spell type enum : Passif / Usable Spell(show only them in the spell bar) ect ect...
        [SerializeField] private BaseSpellTriggerScriptable m_SpellTrigger = null;
        [SerializeField] private int m_BaseCooldown = 0;

        [Header("Spell Level Scaler")] 
        [SerializeField] private BaseSpellLevelScalerScriptable[] m_LevelScalerScriptables = null;
        [SerializeField] private int m_LevelMax = 10;
        
        [Header("Spell Display In Game Option")]
        [SerializeField] private ZoneSelection[] m_Selection = null;
        [SerializeField] private int m_MainSelection = 0;

        [Header("Spell Restriction")]
        [SerializeField] private List<SpellRestriction> m_SpellRestrictions = new List<SpellRestriction>();

        [Header("Spell Rules / Used By Enemy")]
        [SerializeField] private Zone m_AllowedCastZone = null;
        [SerializeField] private SpellOriginType m_OriginSelection = SpellOriginType.ClosestEnemy;
        
        public Zone AllowedCastZone => m_AllowedCastZone;
        public int MainSelection => m_MainSelection;
        public List<SpellRestriction> SpellRestrictions => m_SpellRestrictions;
        public SpellOriginType OriginSelection => m_OriginSelection;
        public ZoneSelection[] Selection => m_Selection;
        public int BaseCooldown => m_BaseCooldown;
        public BaseSpellTriggerScriptable SpellTrigger => m_SpellTrigger;
        public Sprite SpellIcon => m_SpellIcon;
        public Sprite SpellBorder => m_SpellIconBorder;
        public int LevelMax => m_LevelMax;
        public BaseSpellLevelScalerScriptable[] LevelScalerScriptables => m_LevelScalerScriptables;
    }

    [Serializable]
    public class SpellRestriction
    {
        public int SelectionId = 0;
        public SpellRestrictionType Type = SpellRestrictionType.OriginOnEnemy;
    }

    public enum SpellRestrictionType
    {
        OriginOnWalkable,
        FreeTileAroundEnemyTarget,
        IsBowUser,
        OriginOnEnemy,
        CanLaunchSpell,
        CanUseMeleeSpell,
        OriginOnEntityCursed,
        //CanMove,
    }

    public enum SpellOriginType
    {
        //Closest Enemy default//
        ClosestEnemy,
        ClosestAlly,
        RandomWalkableInsideMainSelection,
        ClosestInverseAtDistance,
        Self,
    }
}