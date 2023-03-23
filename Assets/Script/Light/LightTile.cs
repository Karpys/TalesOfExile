using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_LightAffectedRenderer = null;

    private Tile m_AttachedTile = null;

    public Tile Tile => m_AttachedTile;
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
        ApplyLight(false,0);
    }

    public void ApplyLight(bool light,float lightForce)
    {
        foreach (SpriteRenderer spriteRenderer in m_LightAffectedRenderer)
        {
            float lightValue = light ? lightForce : 0;
            spriteRenderer.RoundColor(lightValue);
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
        if (m_ShadowCount == 1 && m_LightCount == 1)
            return true;
        
        return (m_LightCount - m_ShadowCount) + m_HasBeenLightAtLeastOnce <= 0;
    }
}
