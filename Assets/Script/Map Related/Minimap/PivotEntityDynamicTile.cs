using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class PivotEntityDynamicTile : EntityDynamicTile
    {
        protected override void Awake()
        {
            base.Awake();
            MinimapRenderer.Instance.SetPivot(this);
        }
    }
}