using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterpretor:SingletonMonoBehavior<SpellInterpretor>
{
   private bool Validation = false;
   
   private List<Vector2Int> m_DisplayTiles = new List<Vector2Int>();
   public List<Vector2Int> m_ActionTiles = new List<Vector2Int>();

   private SpellData m_CurrentSpell = null;
   private int m_CurrentSpellQueue = 0;
   private List<Vector2Int> m_TilesSelection = new List<Vector2Int>();
   public void LaunchSpellQueue(SpellData spell)
   {
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
            m_TilesSelection = ZoneTileManager.Instance.GetSelectionZone(selection, GetOrigin(selection), selection.Range);
            
            //Highlight Tiles//
            HighlightTilesManager.Instance.HighlightTiles(m_TilesSelection);
            
            if (!m_CurrentSpell.m_Data.m_Selection[m_CurrentSpellQueue].NeedValidation)
            {
               FetchSelection();
            }else if (Validation)
            {
               FetchSelection();
               Validation = false;
            }
         }
         else
         {
            //Trigger Spells//
            //Send List of Tiles Action//
            //The Spell Interpret the data//
            Debug.Log("Trigger Selected Spell");
            ResetSpellQueue();
         }
      }
   }

   private void ResetSpellQueue()
   {
      m_CurrentSpell = null;
      m_CurrentSpellQueue = 0;
      m_ActionTiles.Clear();
      m_DisplayTiles.Clear();
      HighlightTilesManager.Instance.ResetHighlighTilesAndLock();
   }

   private Vector2Int GetOrigin(ZoneSelection selection)
   {
      if (selection.Origin == ZoneOrigin.Self)
      {
         return MapData.Instance.GetPlayerPosition();
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
      for (int i = 0; i < positions.Count; i++)
      {
         m_ActionTiles.Add(positions[i]);
      }
   }
   
   private void AddToDisplayTiles(List<Vector2Int> positions)
   {
      for (int i = 0; i < positions.Count; i++)
      {
         m_DisplayTiles.Add(positions[i]);
      }
   }
}
