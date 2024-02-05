using System;

using UnityEngine;

using Common.Player.Inventory.Model;
using Common.Player.Inventory.View.InventoryItemViews;

using static Common.GameItems.Constants.GameItemConstants;

namespace Common.Player.Inventory.View
{
    public class PlayerInventoryView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject inventoryHolder = null;
        [SerializeField] private Transform inventorySlotsHolderTransform = null;
        [SerializeField] private GameObject inventorySlotViewPrefab = null;
        [SerializeField] private PlayerInventoryItemSlotView[] equipmentSlots = null;
        #endregion

        #region PRIVATE_FIELDS
        private PlayerInventoryItemSlotView[] inventorySlots = null;
        #endregion

        #region ACTIONS
        private Action<bool> onInventoryHolderStatus = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PlayerInventorySlotModel[] playerEquipmentSlots, PlayerInventorySlotModel[] playerInventorySlots, Action<bool> onInventoryHolderStatus)
        {
            this.onInventoryHolderStatus = onInventoryHolderStatus;

            inventorySlots = new PlayerInventoryItemSlotView[playerInventorySlots.Length];

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                PlayerInventoryItemSlotView slotView = Instantiate(inventorySlotViewPrefab, inventorySlotsHolderTransform).GetComponent<PlayerInventoryItemSlotView>();
                inventorySlots[i] = slotView;

                slotView.Configure(playerInventorySlots[i]);
            }

            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].Configure(playerEquipmentSlots[i]);
            }
        }

        public void SetItemToInventorySlot(PlayerInventorySlotModel inventorySlotModel, int index)
        {
            inventorySlots[index].Configure(inventorySlotModel);
        }

        public void SetItemToEquipmentSlot(PlayerInventorySlotModel inventorySlotModel, int index)
        {
            equipmentSlots[index].Configure(inventorySlotModel);
        }

        public void ClearInventorySlot(int index)
        {
            inventorySlots[index].Clear();
        }

        public void ClearEquipmentSlot(GAME_ITEM_SLOT_TYPE type)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].ModelAttached.GameItemInstance.ItemConfigAttached.SlotType == type)
                {
                    equipmentSlots[i].Clear();
                    break;
                }
            }
        }

        public void ToggleInventory()
        {
            SetInventoryStatus(!inventoryHolder.activeSelf);
        }

        public void SetInventoryStatus(bool state)
        {
            onInventoryHolderStatus.Invoke(state);

            inventoryHolder.SetActive(state);
        }
        #endregion
    }
}