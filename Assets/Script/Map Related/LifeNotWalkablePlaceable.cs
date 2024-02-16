namespace KarpysDev.Script.Map_Related
{
    using Manager;
    using UnityEngine;

    public class LifeNotWalkablePlaceable : MapPlaceable,ITurn
    {
        private int m_TurnLeft = 0;
        private TurnObjectCleaner m_Cleaner = null;
        public int TurnLeft => m_TurnLeft;

        public override void Place(Vector2Int position)
        {
            base.Place(position);
            SubToTurnManager();
            MapData.Instance.GetTile(m_Position).Walkable = false;
            m_Cleaner = new TurnObjectCleaner(gameObject, this);
        }

        public void SetTurn(int turn)
        {
            m_TurnLeft = turn;
        }

        public void ReduceTurn()
        {
            m_TurnLeft--;
        }

        public void OnEndLife()
        {
            MapData.Instance.GetTile(m_Position).Walkable = true;
            Destroy(gameObject);
        }

        public void SubToTurnManager()
        {
            TurnManager.Instance.AddTurn(this);
        }
    }
}