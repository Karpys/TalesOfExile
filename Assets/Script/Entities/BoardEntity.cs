using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using KarpysDev.Script.Widget;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    [Serializable]
    public class EntityStats
    {
        private BoardEntity m_AttachedEntity = null;
        //All the EntitiesStats//
        [Header("IA Stats")]
        public int CombatRange = 1;
        [Header("Life Stats")]
        public float MaxLife = 100f;
        public float Life = 100f;
        public float LifeRegeneration = 0;
        //From Damage to move speed to base life ?//
        [Header("Damage Stats")]
        public float WeaponForce = 20f;
    
        //MainTypeModifier//
        [Header("Main Damage Modifier")]
        public float MeleeModifier = 0f;
        public float ProjectileModifier = 0f;
        public float SpellModifier = 0f;
        //SubTypeModifier//
        [Header("Sub Damage Modifier")]
        public float ColdDamageModifier = 0f;
        public float LightningDamageModifier = 0f;
        public float FireDamageModifier = 0f;
        public float PhysicalDamageModifier = 0f;
        public float ElementalDamageModifier = 0f;
        //Physical
        //Ect ect
    
        //DEFENSE//
        [Header("Flat Main Damage Type Reduction")]
        public float FlatMeleeDamageReduction = 0;
        public float FlatProjectileDamageReduction = 0;
        public float FlatSpellDamageReduction = 0;

        [Header("Sub DamageType Percentage Reduction")]
        public float ColdDamageReduction = 0; 
        public float LightningDamageReduction = 0; 
        public float FireDamageReduction = 0; 
        public float PhysicalDamageReduction = 0; 
        public float ElementalDamageReduction = 0;

        [Header("Misc")] 
        public int IsBowUser = 0;
        
        [Header("Crowd Control")]
        public int RootLockCount = 0;
        public int SpellLockCount = 0;
        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetEntity(BoardEntity entity)
        {
            m_AttachedEntity = entity;
        }
        public float GetDamageModifier(SubDamageType subDamageType)
        {
            switch (subDamageType)
            {
                case SubDamageType.Cold:
                    return ColdDamageModifier;
                case SubDamageType.Lightning:
                    return LightningDamageModifier;
                case SubDamageType.Fire:
                    return FireDamageModifier;
                case SubDamageType.Physical:
                    return PhysicalDamageModifier;
                case SubDamageType.Elemental:
                    return ElementalDamageModifier;
                default:
                    Debug.LogError("Sub Damage type not set up :" + subDamageType);
                    return 0;
            }
        }

        public Vector2 GetDamageReduction(MainDamageType mainDamageType,SubDamageType subDamageType)
        {
            float flatReduction = 0;
            float percentageReduction = 0;

            switch (mainDamageType)
            {
                case MainDamageType.Melee:
                    flatReduction = FlatMeleeDamageReduction;
                    break;
                case MainDamageType.Spell:
                    flatReduction = FlatSpellDamageReduction;
                    break;
                case MainDamageType.Projectile:
                    flatReduction = FlatSpellDamageReduction;
                    break;
                default:
                    Debug.LogError("Flat reduction has not been set up");
                    break;
            }

            switch (subDamageType)
            {
                case SubDamageType.Cold:
                    percentageReduction = ColdDamageReduction;
                    break;
                case SubDamageType.Lightning:
                    percentageReduction = LightningDamageReduction;
                    break;
                case SubDamageType.Fire:
                    percentageReduction = FireDamageReduction;
                    break;
                case SubDamageType.Physical:
                    percentageReduction = PhysicalDamageReduction;
                    break;
                case SubDamageType.Elemental:
                    percentageReduction = ElementalDamageReduction;
                    break;
                default:
                    Debug.LogError("Sub Damage type not set up :" + subDamageType);
                    break;
            }

            percentageReduction = Mathf.Min(percentageReduction, 75f);
            return new Vector2(flatReduction, percentageReduction);
        }
    
        public float GetMainTypeModifier(MainDamageType mainDamageType)
        {
            switch (mainDamageType)
            {
                case MainDamageType.Melee:
                    return MeleeModifier;
                case MainDamageType.Projectile:
                    return ProjectileModifier;
                case MainDamageType.Spell:
                    return SpellModifier;
                default:
                    return 0;
            }
        }

        public float GetDamageParametersModifier(DamageParameters damageSpellData,float bonusModifier = 0)
        {
            float modifier = bonusModifier;

            foreach (SubDamageType subDamageType in damageSpellData.DamageType.SubDamageTypes)
            {
                modifier += GetDamageModifier(subDamageType);
            }

            modifier += GetMainTypeModifier(damageSpellData.DamageType.MainDamageType);

            return (modifier + 100) / 100;
        }
    }

    public enum EntityGroup
    {
        Friendly,
        Enemy,
        Neutral,
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
        
            //Spells
            RegisterStartSpells();
        
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
    
        private void RegisterStartSpells()
        {
            for (int i = 0; i < m_EntityData.m_BaseSpellInfos.Length; i++)
            {
                AddSpellToSpellList(m_EntityData.m_BaseSpellInfos[i]);
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
            Debug.Log("Add spell : "  + spell.m_SpellData.SpellKey);
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
                if (spellInfo.Data.SpellKey == spellKey)
                    return spellInfo;
            }
            return null;
        }
        
        public TriggerSpellData GetUsableViaKey(string spellKey)
        {
            foreach (TriggerSpellData spellInfo in UsableSpells)
            {
                if (spellInfo.Data.SpellKey == spellKey)
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