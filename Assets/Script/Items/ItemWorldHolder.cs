using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Manager.Library;
using UnityEngine;

namespace KarpysDev.Script.Items
{
    public class ItemWorldHolder : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_InWorldVisual = null;
        [SerializeField] private Transform m_JumpHolder = null;

        private Item m_Item = null;
        public Item Item => m_Item;
    
        //Pick up variables
        private PlayerBoardEntity m_PlayerControllerEntity = null;
        private Vector2Int m_HolderMapPosition = Vector2Int.zero;
        public Transform JumpHolder => m_JumpHolder;

        private DestroyObjectCleaner m_Cleaner = null;

        private void Start()
        {
            m_PlayerControllerEntity = GameManager.Instance.PlayerEntity;
            GameManager.Instance.A_OnEndTurn += CheckForPickUp;
            m_Cleaner = new DestroyObjectCleaner(gameObject);
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndTurn -= CheckForPickUp;
        }

        //Grab Item Player Inventory//
        private void CheckForPickUp()
        {
            if (m_PlayerControllerEntity.EntityPosition == m_HolderMapPosition)
            {
                if(m_PlayerControllerEntity.PlayerInventory.TryPickUp(m_Item))
                    Destroy(gameObject);
            }
        }
        public void InitalizeHolder(Item item,Vector2Int mapPosition)
        {
            m_Item = item;
            m_HolderMapPosition = mapPosition;
        }
        public void DisplayWorldVisual()
        {
            m_InWorldVisual.sprite = m_Item.Data.InWorldVisual;
            m_InWorldVisual.gameObject.SetActive(true);
        }

        public void OnJumpEnd()
        {
            Instantiate(RarityLibrary.Instance.GetParametersViaKey(m_Item.Rarity).WorldFx, transform);
        }
    }
}