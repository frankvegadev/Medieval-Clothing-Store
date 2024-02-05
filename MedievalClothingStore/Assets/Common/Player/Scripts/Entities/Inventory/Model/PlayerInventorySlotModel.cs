using UnityEngine;

using Common.GameItems.Instance;

namespace Common.Player.Inventory.Model
{
    public class PlayerInventorySlotModel
    {
        #region EXPOSED_FIELDS
        [SerializeField] private bool equipped = false;
        [SerializeField] private GameItemInstanceModel gameItemInstance = null;
        #endregion

        #region PROPERTIES
        public bool Equipped { get => equipped; }
        public GameItemInstanceModel GameItemInstance { get => gameItemInstance; }
        #endregion

        #region PUBLIC_METHODS
        public bool IsSlotOcuppied()
        {
            return gameItemInstance != null;
        }

        public void ClearSlot()
        {
            equipped = false;
            gameItemInstance = null;

            //Update visually on end
        }

        public bool TrySetItemToSlot(GameItemInstanceModel newItemInstance, bool equippedState = false)
        {
            //Item slot already occupied
            if(IsSlotOcuppied())
            {
                return false;
            }

            equipped = equippedState;
            gameItemInstance = newItemInstance;

            //Update visually on end

            return true;
        }

        public void UnequipItem()
        {
            equipped = false;
        }

        public bool TryEquipItem()
        {
            //No item to equip
            if (gameItemInstance == null)
            {
                return false;
            }

            equipped = !equipped;

            return true;
        }
        #endregion
    }
}