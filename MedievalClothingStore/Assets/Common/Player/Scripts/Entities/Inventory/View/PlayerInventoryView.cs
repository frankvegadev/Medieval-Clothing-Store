using UnityEngine;

using Common.Player.Controller.Inventory.Model;

namespace Common.Player.Controller.Inventory.View
{
    public class PlayerInventoryView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerInventoryItemSlotView[] equipmentSlots = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PlayerInventorySlotModel[] playerEquipmentSlots)
        {
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
        #endregion
    }
}