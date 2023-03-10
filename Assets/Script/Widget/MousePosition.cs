using System.Collections.Generic;
using UnityEngine;

public class MousePosition : SingletonMonoBehavior<MousePosition>
{
    // Start is called before the first frame update
    [SerializeField] private bool m_DebugMouseToPlayerTiles = false;
    private Vector2Int m_MouseBoardPosition = Vector2Int.zero;

    public Vector2Int MouseBoardPosition => m_MouseBoardPosition;
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
        //Warning Camera.main//
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        m_MouseBoardPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

        if (m_DebugMouseToPlayerTiles)
        {
            m_DebugMouseToPlayerTiles = false;
            List<Tile> PlayerToMouse = LinePath.GetPathTile(MapData.Instance.GetControlledEntityPosition(), m_MouseBoardPosition);
            HighlightTilesManager.Instance.HighlightTiles(PlayerToMouse.ToPath());
        }
    }
}
