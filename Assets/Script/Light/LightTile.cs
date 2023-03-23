using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_LightAffectedRenderer = null;
    [SerializeField] private TMP_Text m_LightCountTxt = null;
    [SerializeField] private TMP_Text m_ShadowCountTxt = null;

    private Tile m_AttachedTile = null;

    public Tile Tile => m_AttachedTile;

    private bool m_HasBeenDiscovered = false;
    // Start is called before the first frame update
    public bool IsShadow => IsConsideredShadow();
    private int m_LightCount = 0;
    private int m_ShadowCount = 0;
    private int m_HasBeenLightAtLeastOnce = 0;

    public void Init(Tile tile)
    {
        m_AttachedTile = tile;
    }
    void Start()
    {
        ApplyLight(false);
    }

    public void ApplyLight(bool light)
    {
        foreach (SpriteRenderer spriteRenderer in m_LightAffectedRenderer)
        {
            if (light)
            {
                m_HasBeenDiscovered = true;
                spriteRenderer.RoundColor(1);
            }
            else
            {
                if (m_HasBeenDiscovered)
                {
                    spriteRenderer.RoundColor(0.5f);
                }
                else
                {
                    spriteRenderer.RoundColor(0);
                }
            }
        }

        if (m_LightCountTxt)
        {
            m_LightCountTxt.text = m_LightCount.ToString();
            m_ShadowCountTxt.text = m_ShadowCount.ToString();
        }
        ResetLight();
    }

    public void ResetLight()
    {
        m_LightCount  = 0;
        m_ShadowCount  = 0;
        m_HasBeenLightAtLeastOnce = 0;
    }

    public void AddLight()
    {
        if (m_HasBeenLightAtLeastOnce == 0)
            m_HasBeenLightAtLeastOnce += 1;
        m_LightCount  += 1;
    }

    public void AddShadow()
    {
        m_ShadowCount += 1;
    }

    private bool IsConsideredShadow()
    {
        if (m_LightCount == m_ShadowCount)
            return true;
        if (m_LightCount == 1 && m_ShadowCount == 1)
            return true;
        
        return (m_LightCount - m_ShadowCount)  - m_HasBeenLightAtLeastOnce< 0;
    }
}
