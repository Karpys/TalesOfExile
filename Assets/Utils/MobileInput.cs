using System.Collections;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.UI.ItemContainer;
using UnityEngine;
using UnityEngine.UIElements;

public class MobileInput : MonoBehaviour
{
    [SerializeField] private bool m_UseMobileInput = false;
    [SerializeField] private BoardEntityMovement m_EntityMovement = null;
    private Vector2 m_PressPosition = Vector2.zero;
    private Vector2 m_ReleasePosition = Vector2.zero;
        
    void Update()
    {
        if(!m_UseMobileInput)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            m_PressPosition = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            m_ReleasePosition = Input.mousePosition;
            OnRelease();
        }
    }

    private void OnRelease()
    {
        if(Vector2.Distance(m_ReleasePosition,m_PressPosition) < 100)
            return;
        
        //Lock Check//
        if(ItemUIController.Instance.OnMouseHolder && ItemUIController.Instance.OnMouseHolder.MouseOn)
            return;
        
        float angle = Get360AngleFrom(m_PressPosition, m_ReleasePosition);
        m_EntityMovement.TryMoveTo(AngleToVector2OctoDirection(angle));
    }
    
    private float Get360AngleFrom(Vector3 pressPos, Vector3 releasePos)
    {
        Vector3 diff = releasePos - pressPos;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;
        return angle;
    }
    
    private Vector2Int AngleToVector2OctoDirection(float angle)
    {
        if (angle < 22.5f || angle >= 337.5f)
        {
            return new Vector2Int(1, 0);
        }
        else if(angle >= 22.5f && angle < 67.5f)
        {
            return new Vector2Int(1,1);
        }
        else if(angle >= 67.5f && angle < 112.5f)
        {
            return new Vector2Int(0, 1);
        }
        else if(angle >= 112.5f && angle < 157.5)
        {
            return new Vector2Int(-1, 1);
        }
        else if(angle >= 157.5 && angle < 202.5f)
        {
            return new Vector2Int(-1, 0);
        }
        else if(angle >= 202.5f && angle < 247.5f)
        {
            return new Vector2Int(-1, -1);
        }
        else if(angle >= 247.5f && angle < 292.5f)
        {
            return new Vector2Int(0, -1);
        }else if (angle >= 292.5f && angle < 337.5f)
        {
            return new Vector2Int(1, -1);
        }

        return new Vector2Int(0, 0);
    }
}
