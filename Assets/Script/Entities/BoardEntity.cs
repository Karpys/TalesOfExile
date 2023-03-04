using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityStats
{
    private BoardEntity m_AttachedEntity = null;
    //All the EntitiesStats//
    [SerializeField] private float m_MaxLife = 100f;
    [SerializeField] private float m_Life = 100f;
    //From Damage to move speed to base life ?//
    //MainTypeModifier//
    public float MeleeModifier = 0f;
    public float ProjectileModifier = 0f;
    public float SpellModifier = 0f;
    //SubTypeModifier//
    public float ColdDamageModifier = 0f;
    public float HolyDamageModifier = 0f;
    public float FireDamageModifier = 0f;
    //Physical
    //Ect ect
    
    //Getter
    public float Life => m_Life;
    public float MaxLife => m_MaxLife;

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
            case SubDamageType.Holy:
                return HolyDamageModifier;
            case SubDamageType.Fire:
                return FireDamageModifier;
            default:
                return 0;
        }
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

    public void ChangeLifeValue(float value)
    {
        m_Life -= value;
        m_AttachedEntity.A_OnLifeUpdated?.Invoke(m_Life,m_MaxLife);
    }
}

public enum EntityGroup
{
    Friendly,
    Ennemy,
    Neutral,
}
public abstract class BoardEntity : MonoBehaviour
{
    [Header("Base")] 
    [SerializeField] protected BoardEntityDataScriptable m_EntityDataScriptable = null;
    [SerializeField] protected int m_XPosition = 0;
    [SerializeField] protected int m_YPosition = 0;
    // [SerializeField] protected AddDamageModifier m_TestModifier = null;

    protected BoardEntityData m_EntityData = null;
    protected MapData m_TargetMap = null;
    protected EntityEquipement m_Equipement = null;

    public Vector2Int EntityPosition => new Vector2Int(m_XPosition, m_YPosition);
    public Vector3 WorldPosition => m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
    public List<SpellData> Spells => m_EntityData.m_SpellList.m_Spells;
    public EntityStats EntityStats => m_EntityData.m_Stats;
    public EntityGroup EntityGroup => m_EntityData.m_EntityGroup;
    public BoardEntityData EntityData => m_EntityData;
    public EntityEquipement EntityEquipement => m_Equipement;
    
    //Entity Actions//
    public Action<float> A_OnEntityDamageTaken;
    public Action<float,float> A_OnLifeUpdated;

    protected void Awake()
    {
        //Copy Base Entity Data
        m_EntityData = new BoardEntityData(m_EntityDataScriptable.m_EntityBaseData);
        m_EntityData.m_Stats.SetEntity(this);
        
        //Equipement
        m_Equipement = GetComponent<EntityEquipement>();
    }

    protected virtual void Start()
    {
        m_Equipement.InitEquipement();
        Debug.Log("New entity created: " + gameObject.name + "at :" + EntityPosition);
        GameManager.Instance.RegisterEntity(this);
        //Initi Serialize Spells//
        RegisterStartSpells();
    }
    
    //Board Related
    public abstract void EntityAction();
    public void Place(int x, int y, MapData targetMap)
    {
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap = targetMap;
        transform.position = targetMap.GetTilePosition(x, y);
        m_TargetMap.Map.Tiles[x, y].Walkable = false;
    }

    public virtual void MoveTo(int x,int y)
    {
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = false;
        //OnMove ?//
        Movement();
    }
    
    public void MoveTo(Vector2Int pos)
    {
        MoveTo(pos.x,pos.y);
    }

    protected void RemoveFromBoard()
    {
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
    }
    protected virtual void Movement()
    {
        transform.position = m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
    }
    
    //Entity Behaviour Related//
    private void RegisterStartSpells()
    {
        for (int i = 0; i < m_EntityData.m_SpellList.m_Spells.Count; i++)
        {
            m_EntityData.m_SpellList.m_Spells[i] = RegisterSpell(m_EntityData.m_SpellList.m_Spells[i]);
        }
    }

    protected SpellData RegisterSpell(SpellData spell)
    {
        spell.AttachedEntity = this;
        return spell.Initialize();
    }
    
    //Stats Related//
    
    //Spell Related//
    //Player Cast Mainly or Controlled Entity//
    public void CastSpell(TriggerSpellData spellData,SpellTiles spellTiles)
    {
        spellData.SpellTrigger.Trigger(spellData,spellTiles);
    }

    //Cast
    public void CastSpellAt(TriggerSpellData spellData,Vector2Int pos)
    {
        List<List<Vector2Int>> tilesActions = new List<List<Vector2Int>>();
        List<Vector2Int> originTiles = new List<Vector2Int>();

        for (int i = 0; i < spellData.TriggerData.m_Selection.Length; i++)
        {
            ZoneSelection currentSelection = spellData.TriggerData.m_Selection[i];
            
            if (currentSelection.ActionSelection)
            {  
                Vector2Int origin = Vector2Int.zero;
                if (currentSelection.Origin == ZoneOrigin.Self)
                {
                    origin = EntityPosition;
                }
                else
                {
                    origin = pos;
                }
                tilesActions.Add(ZoneTileManager.Instance.GetSelectionZone(currentSelection,origin,currentSelection.Range,EntityPosition));
                originTiles.Add(origin);
            }
        }
        
        CastSpell(spellData,new SpellTiles(originTiles,tilesActions));
    }
    //Damage Related//
    public void TakeDamage(float value)
    {
        m_EntityData.m_Stats.ChangeLifeValue(value);
        A_OnEntityDamageTaken?.Invoke(value);

        if (m_EntityData.m_Stats.Life <= 0)
            TriggerDeath();
    }

    protected abstract void TriggerDeath();

    public DamageSource[] GetAdditionalSources(DamageType damageType)
    {
        //Fetch All Additional Sources / From equipement / skill tree
        List<DamageSource> additionalSources = new List<DamageSource>();
        
        //Test is in local entity
        // DamageSource newSource = m_TestModifier.GetAdditionalDamage(damageType);
        //
        // if(newSource != null)
        //     additionalSources.Add(newSource);
        return additionalSources.ToArray();
    }

    public float GetMainWeaponDamage()
    {
        //TODo: Return the main damage weapon damage
        return 100f;
    }
}