using System.Collections.Generic;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public class MousePosition : SingletonMonoBehavior<MousePosition>
    {
        // Start is called before the first frame update
        [SerializeField] private bool m_DebugMouseToPlayerTiles = false;
        private Vector2Int m_MouseBoardPosition = Vector2Int.zero;
        private Vector3 m_MouseWorldPosition = Vector3.zero;

        public Vector2Int MouseBoardPosition => m_MouseBoardPosition;
        public Vector3 MouseWorldPosition => m_MouseWorldPosition;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                m_DebugMouseToPlayerTiles = true;
            }
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            m_MouseWorldPosition = mousePosition;
            m_MouseBoardPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

            if (m_DebugMouseToPlayerTiles)
            {
                m_DebugMouseToPlayerTiles = false;
                List<Tile> PlayerToMouse = LinePath.GetPathTile(MapData.Instance.GetControlledEntityPosition(), m_MouseBoardPosition,NeighbourType.Square).ToTile();
                HighlightTilesManager.Instance.HighlightTiles(PlayerToMouse.ToPath());
            }
        }
    }
}
