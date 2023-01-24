using UnityEngine;

public class BoardEntityMovement : MonoBehaviour
{
    [SerializeField] private BoardEntity m_Entity = null;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveX(1);
        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveX(-1);
        }else if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveY(1);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveY(-1);
        }
    }

    private void MoveX(int x)
    {
        Vector2Int movement = m_Entity.EntityPosition;
        if(MapData.Instance.IsWalkable(movement.x + x,movement.y))
            m_Entity.MoveTo(movement.x + x,movement.y);
    }
    
    private void MoveY(int y)
    {
        Vector2Int movement = m_Entity.EntityPosition;
        if(MapData.Instance.IsWalkable(movement.x,movement.y + y))
            m_Entity.MoveTo(movement.x ,movement.y + y);
    }
}