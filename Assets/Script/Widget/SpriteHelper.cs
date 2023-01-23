using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHelper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer m_Renderer = null;

    public void SetSpritePriority(int priority)
    {
        m_Renderer.sortingOrder = priority;
    }
}
