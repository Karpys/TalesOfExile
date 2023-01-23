using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] m_Sprite;
    [SerializeField] private SpriteRenderer m_Renderer;
    void Awake()
    {
        m_Renderer.sprite = m_Sprite[Random.Range(0, m_Sprite.Length)];
    }

}
