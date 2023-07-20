using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public abstract class PnjEntity : EntityBehaviour
    {
        private Vector2Int[] m_CloseTiles = null;


        private bool m_IsPlayerClose = false;
        public override void Behave()
        {
            return;
        }

        protected override void InitializeEntityBehaviour()
        {
            m_CloseTiles = TileHelper.GetNeighboursWalkable(MapData.Instance.GetTile(m_AttachedEntity.EntityPosition), NeighbourType.Square, MapData.Instance).ToPath().ToArray();
            GameManager.Instance.A_OnEndTurn += CheckForPlayerPosition;
            MapGenerator.Instance.A_OnMapErased += UnSub;
        }


        protected void CheckForPlayerPosition()
        {
            if (m_CloseTiles.Contains(GameManager.Instance.PlayerEntity.EntityPosition))
            {
                if (!m_IsPlayerClose)
                {
                    m_IsPlayerClose = true;
                    OnPlayerEnterEntity();
                }
            }
            else
            {
                if (m_IsPlayerClose)
                {
                    OnPlayerExit();
                    m_IsPlayerClose = false;
                }
            }
        }

        protected abstract void OnPlayerExit();

        private void UnSub()
        {
            if (GameManager.Instance)
            {
                Debug.Log("Unsub");   
                GameManager.Instance.A_OnEndTurn -= CheckForPlayerPosition;
            }
            MapGenerator.Instance.A_OnMapErased -= UnSub;
        }

        protected abstract void OnPlayerEnterEntity();
    }
}