using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public class PortalMapTileReloader : MapTileReloader
    {
        protected override void OnPlayerOnTile()
        {
            GameManager.Instance.AddLock();
            BoardEntity playerEntity = GameManager.Instance.PlayerEntity;
            playerEntity.VisualTransform.DoRotate(new Vector3(0, 0, 360), 0.5f);
            playerEntity.VisualTransform.DoScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                base.OnPlayerOnTile();
                playerEntity.VisualTransform.DoRotate(new Vector3(0,0,-360), 0.5f);
                playerEntity.VisualTransform.DoScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    GameManager.Instance.ReleaseLock();
                });
            });
        }
    }
}