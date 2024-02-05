using System;

using UnityEngine;

using Common.Player.Inventory.Model;
using Common.Player.Inventory.View.InventoryItemViews;

namespace Common.Player.Inventory.View
{
    public class PlayerInventoryView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject inventoryHolder = null;
        [SerializeField] private PlayerInventoryItemSlotView[] equipmentSlots = null;
        #endregion

        #region ACTIONS
        private Action<bool> onInventoryHolderStatus = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PlayerInventorySlotModel[] playerEquipmentSlots, Action<bool> onInventoryHolderStatus)
        {
            this.onInventoryHolderStatus = onInventoryHolderStatus;

            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].Configure(playerEquipmentSlots[i]);
            }
        }

        public void SetItemToEquipmentSlot(PlayerInventorySlotModel inventorySlotModel)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].ModelAttached == inventorySlotModel)
                {
                    equipmentSlots[i].Configure(inventorySlotModel);
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