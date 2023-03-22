using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_LightAffectedRenderer = null;
    // Start is called before the first frame update
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
    }
}
