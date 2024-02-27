using System;
using System.Collections.Generic;
using KarpysDev.Script.DamgeType;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    using Items;
    using KarpysUtils;
    using KarpysUtils.TweenCustom;
    using Spell.DamageSpell;

    [Serializable]
    public class EntityStats
    {
        private BoardEntity m_AttachedEntity = null;
        //All the EntitiesStats//
        [Header("IA Stats")]
        [SerializeField] private int m_CombatRange = 1;
        [Header("Life Stats")]
        [SerializeField] private float m_MaxLife = 100f;
        [SerializeField] private float m_Life = 100f;
        [SerializeField] private float m_LifeRegeneration = 0;
        //From Damage to move speed to base life ?//
        [Header("Damage Stats")]
        [SerializeField] private float m_WeaponForce = 20f;

        [Header("Weapon Damage Stats")] 
        private List<IWeapon> m_WeaponItems = new List<IWeapon>();
        private IWeapon m_MainHandWeapon = null;
        private IWeapon m_OffHandWeapon = null;

        //SubTypeModifier//
        [Header("Sub Damage Modifier")]
        [SerializeField] private SubDamageTypeGroup m_DamageTypeModifier = null;

        //DEFENSE//
        [Header("Sub DamageType Percentage Reduction")]
        [SerializeField] private SubDamageTypeGroup m_DamageTypeReduction = null;

        [Header("Misc")] 
        [SerializeField] private int m_IsBowUser = 0;
        
        [Header("Crowd Control")]
        [SerializeField] private int m_RootLockCount = 0;
        [SerializeField] private int m_SpellLockCount = 0;
        [SerializeField] private int m_MeleeLockCount = 0;

        #region Properties
        public float MaxLife { get => m_MaxLife; set => m_MaxLife = value; }
        public float Life { get => m_Life; set => m_Life = value; }
        public float LifeRegeneration { get => m_LifeRegeneration; set => m_LifeRegeneration = value; }
        public float WeaponForce { get => m_WeaponForce; set => m_WeaponForce = value; }
        public int RootLockCount { get => m_RootLockCount; set => m_RootLockCount = value;}
        public int SpellLockCount { get => m_SpellLockCount; set => m_SpellLockCount = value;}
        public int MeleeLockCount { get => m_MeleeLockCount; set => m_MeleeLockCount = value;}
        public int CombatRange { get => m_CombatRange; set => m_CombatRange = value;}
        public int IsBowUser { get => m_IsBowUser; set => m_IsBowUser = value;}
        public SubDamageTypeGroup DamageTypeModifier => m_DamageTypeModifier;
        public SubDamageTypeGroup DamageTypeReduction => m_DamageTypeReduction;
        public IWeapon MainHandWeapon => m_MainHandWeapon;
        public IWeapon OffHandWeapon => m_OffHandWeapon;
        public List<IWeapon> WeaponItems => m_WeaponItems;

        #endregion
        public EntityStats(EntityStats stats)
        {
            m_Life = stats.Life;
            m_MaxLife = stats.MaxLife;
            m_LifeRegeneration = stats.LifeRegeneration;
            m_CombatRange = stats.CombatRange;
            m_WeaponForce = stats.CombatRange;
            m_RootLockCount = stats.RootLockCount;
            m_MeleeLockCount = stats.MeleeLockCount;
            m_SpellLockCount = stats.SpellLockCount;
            m_DamageTypeModifier = new SubDamageTypeGroup(stats.DamageTypeModifier);
            m_DamageTypeReduction = new SubDamageTypeGroup(stats.DamageTypeReduction);
            m_IsBowUser = stats.IsBowUser;
        }

        public void SetEntity(BoardEntity entity)
        {
            m_AttachedEntity = entity;
        }
        public float GetDamageModifier(SubDamageType subDamageType)
        {
            float damageModifier = m_DamageTypeModifier.GetTypeValue(subDamageType);
            //Todo: Add Global Damage Modifier like All Damage//
            return damageModifier;
        }

        public float GetPercentageDamageReduction(SubDamageType subDamageType)
        {
            float percentageReduction = m_DamageTypeReduction.GetTypeValue(subDamageType);
            percentageReduction = Mathf.Min(percentageReduction, 75f);
            return percentageReduction;
        }
        
        public float GetDamagePenetration(SubDamageType subDamageType)
        {
            switch (subDamageType)
            {
                case SubDamageType.Physical:
                    break;
                case SubDamageType.Fire:
                    break;
                case SubDamageType.Cold:
                    break;
                case SubDamageType.Lightning:
                    break;
                case SubDamageType.Nature:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(subDamageType), subDamageType, null);
            }

            return 0;
        }
    
        public void AddStunLock(int stunLockState)
        {
            m_RootLockCount += stunLockState;
            m_MeleeLockCount += stunLockState;
            m_SpellLockCount += stunLockState;
        }

        public void AddWeapon(IWeapon weaponItem, WeaponTarget target)
        {
            switch (target)
            {
                case WeaponTarget.MainWeapon:
                    m_MainHandWeapon = weaponItem;
                    break;
                case WeaponTarget.OffWeapon:
                    m_OffHandWeapon = weaponItem;
                    break;
            }

            m_WeaponItems.Add(weaponItem);
        }

        public void RemoveWeapon(IWeapon weaponItem, WeaponTarget target)
        {
            switch (target)
            {
                case WeaponTarget.MainWeapon:
                    m_MainHandWeapon = null;
                    break;
                case WeaponTarget.OffWeapon:
                    m_OffHandWeapon = null;
                    break;
            }

            m_WeaponItems.Remove(weaponItem);
        }
    }

    public enum EntityGroup
    {
        Friendly,
        Enemy,
        Neutral,
        All,
        None,
    }
    public abstract class BoardEntity : MonoBehaviour
    {
        [Header("Base")] 
        [SerializeField] protected BoardEntityDataScriptable m_EntityDataScriptable = null;
        [SerializeField] protected Transform m_VisualTransform = null;
        [SerializeField] protected bool m_Targetable = true;
        // [SerializeField] protected AddDamageModifier m_TestModifier = null;
        protected bool m_CanBehave = true;
        protected bool m_IsDead = false;
        protected int m_YPosition = 0;
        protected int m_XPosition = 0;
    
        protected EntityBehaviour m_EntityBehaviour = null;
        [SerializeField] protected BoardEntityData m_EntityData = null;
    
        protected MapData m_TargetMap = null;
        protected EntityBuffs m_Buffs = null;
        protected BoardEntityLife m_EntityLife = null;
        protected BoardEntityEventHandler m_EntityEvent = null;
        protected List<TriggerSpellData> m_Spells = new List<TriggerSpellData>();

        public Action A_OnEntityInitialization = null;

        public MapData Map => m_TargetMap;
        public Vector2Int EntityPosition => new Vector2Int(m_XPosition, m_YPosition);
        public Vector3 WorldPosition => m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
        public List<TriggerSpellData> Spells => m_Spells;
        public List<TriggerSpellData> UsableSpells => GetUsableSpells();
        public EntityStats EntityStats => m_EntityData.m_Stats;
        public EntityGroup EntityGroup => m_EntityData.m_EntityGroup;
        public EntityGroup TargetEntityGroup => m_EntityData.m_TargetEntityGroup;
        public BoardEntityData EntityData => m_EntityData;
        public Transform VisualTransform => m_VisualTransform;
        public EntityBuffs Buffs => m_Buffs;
        public BoardEntityLife Life => m_EntityLife;
        public BoardEntityEventHandler EntityEvent => m_EntityEvent;
    
        //Property//
        public bool Targetable => m_Targetable;
        
        //Private Field//
        private BoardEntity m_LastGetHit = null;

        //Need to be called when an entity is created//
        public virtual void EntityInitialization(EntityBehaviour entityIa,EntityGroup entityGroup,EntityGroup targetEntityGroup = EntityGroup.None)
        {
            //Copy Base Entity Data
            m_EntityData = new BoardEntityData(m_EntityDataScriptable.m_EntityBaseData);
            m_EntityData.m_Stats.SetEntity(this);
            m_EntityData.m_EntityGroup = entityGroup;
            m_EntityData.m_TargetEntityGroup = targetEntityGroup == EntityGroup.None
                ? EntityHelper.GetInverseEntityGroup(entityGroup)
                : targetEntityGroup;
        
            //Event//
            m_EntityEvent = GetComponent<BoardEntityEventHandler>();
        
            //Life
            m_EntityLife = GetComponent<BoardEntityLife>();
            m_EntityLife.Initialize(m_EntityData.m_Stats.MaxLife, m_EntityData.m_Stats.Life,m_EntityData.m_Stats.LifeRegeneration,this);

            //Buffs
            m_Buffs = GetComponent<EntityBuffs>();
        
            //OnEntityInitilizationEnd
            A_OnEntityInitialization?.Invoke();
            
            //Spells
            RegisterStartSpells(m_EntityData.m_BaseSpellInfos);
        
            //Entity Behaviour//
            InitializeEntityBehaviour(entityIa);
        
            //Entity Registration//
            RegisterEntity();
        }

        private void Start()
        {
            Debug.Log("New entity created: " + gameObject.name + "at :" + EntityPosition);
        }

        protected virtual void InitializeEntityBehaviour(EntityBehaviour entityIa)
        {
            SetEntityBehaviour(entityIa);
        }

        protected virtual void RegisterEntity()
        {
            GameManager.Instance.RegisterEntity(this);
        }
    
    
        //Board Related
        public virtual void EntityAction()
        {
            if(m_CanBehave)
                m_EntityBehaviour.Behave();   
        }
        public void Place(int x, int y, MapData targetMap)
        {
            transform.DoKill();
            m_XPosition = x;
            m_YPosition = y;
            m_TargetMap = targetMap;
            transform.position = targetMap.GetTilePosition(x, y);
            OnNewPosition(EntityPosition);
        }

        public virtual void MoveTo(int x, int y, bool movement = true)
        {
            m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
            m_TargetMap.Map.EntitiesTile[m_XPosition, m_YPosition] = null;
            m_XPosition = x;
            m_YPosition = y;
            OnNewPosition(EntityPosition);
            //OnMove ?//
            if(movement)
                Movement();
        }

        protected virtual void OnNewPosition(Vector2Int position)
        {
            m_TargetMap.Map.Tiles[position.x,position.y].Walkable = false;
            m_TargetMap.Map.EntitiesTile[position.x, position.y] = this;
        }
    
        public void MoveTo(Vector2Int pos,bool movement = true)
        {
            MoveTo(pos.x,pos.y,movement);
        }

        public void SetEntityBehaviour(EntityBehaviour entityBehaviour)
        {
            m_EntityBehaviour = entityBehaviour;
            m_EntityBehaviour.SetEntity(this);
        }
    
        protected virtual void RemoveFromBoard()
        {
            m_TargetMap.Map.EntitiesTile[m_XPosition,m_YPosition] = null;
            m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
        }
        protected virtual void Movement()
        {
            transform.position = m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
        }
    
        //Entity Behaviour Related//
        public void SetBehaveState(bool canBehave)
        {
            m_CanBehave = canBehave;
        }
    
        protected virtual void RegisterStartSpells(SpellInfo[] spellInfos)
        {
            for (int i = 0; i < spellInfos.Length; i++)
            {
                AddSpellToSpellList(spellInfos[i]);
            }
        }

        public void UpdateSpellPriority()
        {
            if (m_EntityBehaviour is BaseEntityIA baseEntity)
            {
                baseEntity.ComputeSpellPriority();       
            }
        }

        public TriggerSpellData RegisterSpell(SpellInfo spell)
        {
            TriggerSpellData spellData = new SpellData(spell,this).Initialize(spell,this) as TriggerSpellData;
            return spellData;
        }
        
        protected virtual List<TriggerSpellData> GetUsableSpells()
        {
            return m_Spells;
        }

        public virtual TriggerSpellData AddSpellToSpellList(SpellInfo spell)
        {
            Debug.Log("Add spell : "  + spell.SpellData.SpellName + " : Level : " + spell.InitialSpellLevel);
            TriggerSpellData spellAdded = RegisterSpell(spell);
            m_Spells.Add(spellAdded);
            return spellAdded;
        }

        public virtual void RemoveSpellToSpellList(TriggerSpellData spell)
        {
            m_Spells.Remove(spell);
        }

        public TriggerSpellData GetSpellViaKey(string spellKey)
        {
            foreach (TriggerSpellData spellInfo in Spells)
            {
                if (spellInfo.Data.SpellName == spellKey)
                    return spellInfo;
            }
            return null;
        }
        
        public TriggerSpellData GetUsableViaKey(string spellKey)
        {
            foreach (TriggerSpellData spellInfo in UsableSpells)
            {
                if (spellInfo.Data.SpellName == spellKey)
                    return spellInfo;
            }
            return null;
        }

        public void ReduceAllCooldown()
        {
            foreach (TriggerSpellData triggerData in Spells)
            {
                triggerData.ReduceCooldown();       
            }
        }

        public virtual TriggerSpellData[] GetDisplaySpells()
        {
            m_Spells.Count.Log("Spell Count");
            return m_Spells.ToArray();
        }

        //Need to be used when the entity is buffed / Equip / Unequip items//
        public virtual void ComputeAllSpells()
        {
            foreach (TriggerSpellData spellData in Spells)
            {
                spellData.SpellTrigger.ComputeSpellData(this);
            }

            m_EntityEvent.OnSpellRecompute?.Invoke();
        }
    
        //Damage Related//
        public void HealTarget(float value)
        {
            m_EntityLife.ChangeLifeValue(value);
        }
        public void TakeDamage(float value)
        {
            m_EntityLife.ChangeLifeValue(-value);
        }
        
        public void TakeDamage(float value,BoardEntity damageFrom)
        {
            m_LastGetHit = damageFrom;
            m_EntityLife.ChangeLifeValue(-value);
        }

        public virtual void TriggerDeath()
        {
            if (m_IsDead)
                return;
        
            m_IsDead = true;
        
            if(m_EntityData.m_EntityGroup == EntityGroup.Enemy)
                GameManager.Instance.UnRegisterActiveEnemy(this);
            GameManager.Instance.UnRegisterEntity(this);
        
            RemoveFromBoard();
            m_EntityEvent.OnDeath?.Invoke();

            if (m_LastGetHit)
            {
                m_LastGetHit.m_EntityEvent.OnKill?.Invoke(this);
            }
        
            Destroy(gameObject);
        }
        public void ForceDeath(bool triggerDeathEvent = false)
        {
            //Remove Death Event / Remove Loot Spawn on map Reload
            if(!triggerDeathEvent)
                m_EntityEvent.OnDeath = null;
        
            TriggerDeath();
        }

        public virtual float GetMainWeaponDamage()
        {
            return EntityStats.WeaponForce;
        }
    }
}