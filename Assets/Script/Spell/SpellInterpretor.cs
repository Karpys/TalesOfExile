using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterpretor:SingletonMonoBehavior<SpellInterpretor>
{
   [SerializeField] private Color m_ActionColor = Color.white;
   [SerializeField] private Color m_DisplayColor = Color.white;
   
   private bool Validation = false;
   
   private List<List<Vector2Int>> m_DisplayTiles = new List<List<Vector2Int>>();
   private List<List<Vector2Int>> m_ActionTiles = new List<List<Vector2Int>>();
   private List<Vector2Int> m_OriginTiles = new List<Vector2Int>();

   private SpellData m_CurrentSpell = null;
   private int m_CurrentSpellQueue = 0;
   
   private Vector2Int m_OriginTile = Vector2Int.zero;
   private List<Vector2Int> m_TilesSelection = new List<Vector2Int>();
   public void LaunchSpellQueue(SpellData spell)
   {
      ResetSpellQueue();
      
      m_CurrentSpell = spell;
      m_CurrentSpellQueue = 0;
      //Launch Spell Queue

   }

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.V))
      {
         Validation = true;
      }
      
      //Spell Queue Update Loop
      SpellQueueUpdate();
   }

   private void SpellQueueUpdate()
   {
      if (m_CurrentSpell != null)
      {
         if (m_CurrentSpellQueue < m_CurrentSpell.m_Data.m_Selection.Length)
         {
            //Get the current Zone Selection following the ZoneTileManager seleciton rules based on Zone Selection Class//
            ZoneSelection selection = m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue];
            m_OriginTile = GetOrigin(selection);
            m_TilesSelection = ZoneTileManager.Instance.GetSelectionZone(selection, m_OriginTile, selection.Range);
            
            //Highlight Tiles//
            Color tilesColor = GetColor(selection);
            HighlightTilesManager.Instance.HighlightTiles(m_TilesSelection,tilesColor);
            
            if (!m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue].ValidationType.NeedValidation)
            {
               FetchSelection();
            }else if (Validation)
            {
               if (CanValidate(m_OriginTile))
               {
                  FetchSelection();
                  Validation = false;
               }
               else
               {
                  Validation = false;
               }
            }
         }
         else
         {
            //Trigger Spells//
            //Send List of Tiles Action//
            if (GameManager.Instance.CanPlay)
            {
               GameManager.Instance.ControlledEntity.CastSpell(m_CurrentSpell,new SpellTiles(m_OriginTiles,m_ActionTiles));
               GameManager.Instance.A_OnPlayerAction.Invoke(m_CurrentSpell.AttachedEntity);
               ResetSpellQueue();
            }
         }
      }
   }

   private bool CanValidate(Vector2Int validationOrigin)
   {
      if (m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue].ValidationType.TargetZoneValidation == -1)
         return true;

      //if the origin is in the display list of the id => Valid current selection//
      if (m_DisplayTiles[m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue].ValidationType.TargetZoneValidation]
          .Contains(validationOrigin))
      {
         return true;
      }
      else
         return false;
   }

   private void ResetSpellQueue()
   {
      m_CurrentSpell = null;
      m_CurrentSpellQueue = 0;
      m_ActionTiles.Clear();
      m_DisplayTiles.Clear();
      HighlightTilesManager.Instance.ResetHighlighTilesAndLock();
   }
   
   private Color GetColor(ZoneSelection selection)
   {
      if (selection.ActionSelection)
      {
         return m_ActionColor;
      }
      else
      {
         return m_DisplayColor;
      }
   }

   private Vector2Int GetOrigin(ZoneSelection selection)
   {
      if (selection.Origin == ZoneOrigin.Self)
      {
         return MapData.Instance.GetControlledEntityPosition();
      }
      else
      {
         return MousePosition.Instance.MouseBoardPosition;
      }
   }

   public void FetchSelection()
   {
      //Add it to the action selection...Future use for spell trigger damage / effect//
      if (m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue].ActionSelection)
      {
         AddToActionTiles(m_TilesSelection);
         AddToOriginTiles(m_OriginTile);
      }
      else
      {
         AddToDisplayTiles(m_TilesSelection);
      }

      HighlightTilesManager.Instance.LockHighLightTiles(m_TilesSelection.Count);
      m_CurrentSpellQueue += 1;
   }

   private void AddToActionTiles(List<Vector2Int> positions)
   {
      m_ActionTiles.Add(positions);
   }

   private void AddToOriginTiles(Vector2Int originTile)
   {
      m_OriginTiles.Add(originTile);
   }
   
   private void AddToDisplayTiles(List<Vector2Int> positions)
   {
      m_DisplayTiles.Add(positions);
   }
}
