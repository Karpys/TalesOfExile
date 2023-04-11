using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WeightElementDraw<Sprite> m_SpriteDraw = null;
    [SerializeField] private SpriteRenderer m_Renderer;
    void Awake()
    {
        m_Renderer.sprite = m_SpriteDraw.Draw();
    }

}
