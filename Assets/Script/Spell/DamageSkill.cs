using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Damage Spell", menuName = "New Spell Trigger", order = 0)]
public class DamageSkill : BaseSpellTriggerScriptable
{
    public override void Trigger(SpellData spellData,List<List<Vector2Int>> actionTiles)
    {
        //Apply Damage To All Actions Tiles//
        for (int i = 0; i < actionTiles.Count; i++)
        {
            for (int j = 0; j < actionTiles[i].Count; j++)
            {
                //Damage Entity At actionTile Pos//
                BoardEntity damageTo = null;
                DamageManager.Instance.TryDamageEnnemy(actionTiles[i][j],spellData.AttachedEntity,out damageTo);
            }
        }
        return;
    }
}