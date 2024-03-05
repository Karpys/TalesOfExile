using System;
using System.Collections.Generic;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using UnityEngine;

public class LightTest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Renderer = null;
    [SerializeField] private Color m_FogColor = Color.white;
    [SerializeField] private Color m_PlayerColor = Color.white;
    [SerializeField] private Color m_DiscoverFog = Color.white;
    [SerializeField] private Zone m_Zone = null;
    [SerializeField] private FilterMode m_FilterMode = FilterMode.Trilinear;
    
    private Texture2D m_Texture = null;
    private Sprite m_Sprite = null;

    private void Start()
    {
        GameManager.Instance.A_OnEndTurn += UpdateLight;
    }

    private List<Vector2Int> m_LastLight = new List<Vector2Int>();

    private void UpdateLight()
    {
        foreach (Vector2Int lastLight in m_LastLight)
        {
            m_Texture.SetPixel(lastLight.x,lastLight.y,m_DiscoverFog);
        }
        
        
        Vector2Int playerPos = GameManager.Instance.PlayerEntity.EntityPosition;
        List<Vector2Int> lightTile = ZoneTileManager.GetSelectionZone(m_Zone, playerPos, m_Zone.Range);

        foreach (Vector2Int lightPos in lightTile)
        {
            m_Texture.SetPixel(lightPos.x,lightPos.y,m_PlayerColor);
        }

        m_LastLight = lightTile;
        
        m_Texture.Apply();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateTexture();
        }
    }

    private void CreateTexture()
    {
        m_Texture = new Texture2D(MapData.Instance.Map.Width, MapData.Instance.Map.Height);
        m_Sprite = Sprite.Create(m_Texture,new Rect(Vector2.zero,new Vector2(m_Texture.width,m_Texture.height)),new Vector2(0.5f,0.5f),1);
        m_Renderer.sprite = m_Sprite;
        
        for (int x = 0; x < m_Texture.width; x++)
        {
            for (int y = 0; y < m_Texture.height; y++)
            {
                m_Texture.SetPixel(x,y,m_FogColor);
            }
        }

        m_Texture.filterMode = m_FilterMode;
        m_Renderer.transform.position = new Vector3((float) m_Texture.width / 2 - .5f, (float) m_Texture.height / 2 - .5f);
        m_Texture.Apply();
    }
}
