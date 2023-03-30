using System;
using System.Collections.Generic;
using TweenCustom;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlightCore : WorldTile
{
    [SerializeField] private ZoneSelection m_OuterZoneSelection = null;
    [SerializeField] private TileSet m_BranchTileSet = null;
    [SerializeField] private int m_BranchCount = 1;
    [SerializeField] private BlightSpawner m_BlightSpawner = null;
    [SerializeField] private Transform m_BlightPopup = null;
    [SerializeField] private Button m_BeginBlightButton = null;
    [SerializeField] private Button m_NoBlightButton = null;

    private BlightSpawner[] m_Spawners = null;
    private bool m_BlightPopupShow = false;
    private bool m_BlightStarted = false;

    private int m_BlightMonsterCount = 0;
    private void Start()
    {
        GameManager.Instance.A_OnEndTurn += CheckForActive;
        m_BeginBlightButton.onClick.AddListener(OnBeginClicked);
        m_NoBlightButton.onClick.AddListener(OnNoClicked);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= CheckForActive;
        m_BeginBlightButton.onClick.RemoveListener(OnBeginClicked);
        m_NoBlightButton.onClick.RemoveListener(OnNoClicked);
    }
    
    public void Initalize(Map map)
    {
        List<Vector2Int> m_outerSelection = ZoneTileManager.GetSelectionZone(m_OuterZoneSelection, m_AttachedTile.TilePosition, m_OuterZoneSelection.Range);
        List<WorldTile> branchTiles = new List<WorldTile>();
        List<SpriteRenderer> branchRenderers = new List<SpriteRenderer>();
        m_Spawners = new BlightSpawner[m_BranchCount];

        for (int i = 0; i < m_BranchCount; i++)
        {
            int randomOuterSelection = Random.Range(0, m_outerSelection.Count);
            Vector2Int outerPosition = MapData.Instance.MapClampedPosition(m_outerSelection[randomOuterSelection]);

            LinePath.NeighbourType = NeighbourType.Cross;
            List<WorldTile> branchPath = new List<WorldTile>();
            branchPath.Add(this);
            branchPath.AddRange(LinePath.GetPathTile(Tile.TilePosition,outerPosition).ToTile().ToWorldTile());
            LinePath.NeighbourType = NeighbourType.Square;

            foreach (WorldTile branchTile in branchPath)
            {
                branchRenderers.Add(map.InsertVisualTile(m_BranchTileSet.TilePrefab,branchTile).Renderer);
            }
            
            InsertBranchExtremity(branchPath,i,map);
            
            m_outerSelection.Remove(outerPosition);
            branchTiles.AddRange(branchPath);
        }
        
        TileHelper.GenerateTileSet(branchTiles,branchRenderers,m_BranchTileSet.TileMap,MapData.Instance);
    }

    private void InsertBranchExtremity(List<WorldTile> branchPath,int id,Map map)
    {
        WorldTile lastWorldTile = branchPath[branchPath.Count - 1];
        BlightSpawner blightSpawner = (BlightSpawner)map.PlaceTileAt(m_BlightSpawner, lastWorldTile.Tile.XPos, lastWorldTile.Tile.YPos);
        branchPath[branchPath.Count - 1] = blightSpawner;
        blightSpawner.Initialize(branchPath.ToTile(),this);
        m_Spawners[id] = blightSpawner;
    }

    private void CheckForActive()
    {
        if(m_BlightStarted)
            return;
        
        Vector2Int playerPosition = GameManager.Instance.PlayerEntity.EntityPosition;
        if (DistanceUtils.GetSquareDistance(playerPosition, Tile.TilePosition) <= 1)
        {
            DisplayBlightPopup(true);
        }
        else
        {
            DisplayBlightPopup(false);
        }
    }

    private void DisplayBlightPopup(bool active)
    {
        m_BlightPopup.DoKill();
        if (active)
        {
            m_BlightPopup.gameObject.SetActive(true);
            GameManager.Instance.AddLock();
            m_BlightPopupShow = true;
            m_BlightPopup.localScale = new Vector3(0, 0, 0);
            m_BlightPopup.DoScale(Vector3.one, 0.2f).SetEase(Ease.EASE_OUT_SIN);
        }
        else
        {
            m_BlightPopup.DoScale(Vector3.zero, 0.2f).SetEase(Ease.EASE_OUT_SIN)
            .OnComplete(() =>
            {
                m_BlightPopup.gameObject.SetActive(true);
            } );
        }
    }

    private void OnBeginClicked()
    {
        BeginBlight();
        GameManager.Instance.ReleaseLock();
    }

    private void BeginBlight()
    {
        DisplayBlightPopup(false);

        foreach (BlightSpawner spawner in m_Spawners)
        {
            spawner.ActiveBlight(true);
            m_BlightMonsterCount += spawner.MonsterCount;
        }

        m_BlightStarted = true;
    }

    private void OnNoClicked()
    {
        DisplayBlightPopup(false);
        GameManager.Instance.ReleaseLock();
    }

    public void ReduceBlightCount()
    {
        m_BlightMonsterCount -= 1;

        if (m_BlightMonsterCount <= 0)
            TriggerBlightWin();
    }

    private void TriggerBlightWin()
    {
        
    }
}