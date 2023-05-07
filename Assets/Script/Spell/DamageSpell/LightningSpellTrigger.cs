using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Special That needed a lot a works//
//Use on tile hit / Entity hit animation as line renderer Animation parameters//
//On entity hit get entity striked around last/Entity hit (or striked)//
//Then call DamageEntity//
//Cant call base EntityHit, will cause pseudo infinite loop//

//Things to change to make this class clean//
//Make the entity selection inside the Trigger//
//Call the base.Entity foreach entityStriked, need to check if they still exist//
//For the LineRenderer Add A SpellAnimationLibrary//
//Don't fix what's not broken//
//Todo: Flag as todo but works great//
public class LightningSpellTrigger : DamageSpellTrigger
{
    private Zone m_ZoneStrike = null;
    private int m_StrikeCount = 0;
    public LightningSpellTrigger(DamageSpellScriptable damageSpellData,ZoneType zoneType,int zoneRange,int strikeCount) : base(damageSpellData)
    {
        m_ZoneStrike = new Zone(zoneType, zoneRange);
        m_StrikeCount = strikeCount;
    }


    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,
        Vector2Int origin, CastInfo castInfo)
    {
        List<BoardEntity> targetEntity = GameManager.Instance.GetEntityViaGroup(targetGroup);
        targetEntity.Remove(entity);


        List<BoardEntity> entityStriked = new List<BoardEntity> {entity};
        entityStriked.AddRange(DistanceUtils.GetZoneContactEntity(m_ZoneStrike, targetEntity, entity.EntityPosition, m_StrikeCount));

        for (int i = 0; i < entityStriked.Count; i++)
        {
            BoardEntity en = entityStriked[i];
            if (en != null)
            {
                base.EntityHit(en,spellData,targetGroup,origin,castInfo);
            }
        }

        //Animation//
        List<Vector2Int> strikePoints = new List<Vector2Int>{spellData.AttachedEntity.EntityPosition};
        strikePoints.AddRange(entityStriked.Select(en => en.EntityPosition).ToList());
        LineRenderer.LinePointsRenderer(strikePoints, LineRendererType.Lighning, 0.3f, 0.01f);

        m_SpellAnimDelay = 0.3f;
    }
}