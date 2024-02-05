using System;

using UnityEngine;

using Common.GameItems.Config;
using Common.GameItems.Instance;

using Common.NPC.Model;

using Common.Player.Inventory.Model;
using Common.Player.Inventory.View;

using static Common.GameItems.Constants.GameItemConstants;

namespace Common.Player.Inventory
{
    public class PlayerInventoryHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerInventoryView inventoryView = null;
        [SerializeField] private NPCClothesModel defaultPlayerClothes = null;
        #endregion

        #region PRIVATE_FIELDS
        private PlayerInventorySlotModel[] playerEquipmentSlots = null;
        #endregion

        #region ACTIONS
        private Action<GameItemConfig> onUpdatePlayerClothes = null;
        #endregion

        #region PROPERTIES
        public PlayerInventorySlotModel[] PlayerEquipmentSlots { get => playerEquipmentSlots; }
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<GameItemConfig> onUpdatePlayerClothes)
        {
            this.onUpdatePlayerClothes = onUpdatePlayerClothes;

            playerEquipmentSlots = new PlayerInventorySlotModel[(int)GAME_ITEM_SLOT_TYPE.NONE];

            for (int i = 0; i < playerEquipmentSlots.Length; i++)
            {
                playerEquipmentSlots[i] = new PlayerInventorySlotModel();
            }

            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.HAIR, CreateItemInstance(defaultPlayerClothes.HairConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.TORSO, CreateItemInstance(defaultPlayerClothes.TorsoConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.LEGS, CreateItemInstance(defaultPlayerClothes.LegsConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.FEET, CreateItemInstance(defaultPlayerClothes.FeetConfig));

            inventoryView.Configure(playerEquipmentSlots);
        }

        public void TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE equipmentSlotType, GameItemInstanceModel gameItemInstance)
        {
            //Add swap later

            if (playerEquipmentSlots[(int)equipmentSlotType].TrySetItemToSlot(gameItemInstance, true))
            {
                //Clear from regular inventory slot

                onUpdatePlayerClothes.Invoke(gameItemInstance.ItemConfigAttached);
                inventoryView.SetItemToEquipmentSlot(playerEquipmentSlots[(int)equipmentSlotType]);
            }
        }

        public GameItemInstanceModel CreateItemInstance(GameItemConfig itemConfig)
        {
            return new GameItemInstanceModel(itemConfig);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}