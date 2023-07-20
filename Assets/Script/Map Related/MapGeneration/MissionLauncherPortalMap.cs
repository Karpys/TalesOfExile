
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related.QuestRelated;
using KarpysDev.Script.UI;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public class MissionLauncherPortalMap : MapTileReloader
    {
        [SerializeField] private SpriteRenderer m_Visual = null;
        private Quest m_Quest = null;
        public void Init(Sprite sprite,Quest quest)
        {
            m_Visual.sprite = sprite;
            m_Quest = quest;
            MissionSelectionManager.Instance.SetPortal(this);
        }
        protected override void OnPlayerOnTile()
        {
            MissionSelectionManager.Instance.CloseAll();
            MapGenerator.Instance.SetMapGroup(m_Quest.MapGroup);   
            MissionSelectionManager.Instance.SetQuest(m_Quest);
            
            //PlayerAnimation
            GameManager.Instance.AddLock();
            BoardEntity playerEntity = GameManager.Instance.PlayerEntity;
            playerEntity.VisualTransform.DoRotate(new Vector3(0, 0, 360), 0.5f);
            playerEntity.VisualTransform.DoScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                MapGenerator.Instance.LaunchMap(); 
                playerEntity.VisualTransform.DoRotate(new Vector3(0,0,-360), 0.5f);
                playerEntity.VisualTransform.DoScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    GameManager.Instance.ReleaseLock();
                });
            });
        }
    }
}