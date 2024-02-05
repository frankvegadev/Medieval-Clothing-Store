using System;
using System.Collections.Generic;

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
        private PlayerInventorySlotModel[] playerInventorySlots = null;

        private string toggleInventoryInputName = string.Empty;
        #endregion

        #region CONSTANTS
        private const int playerSlots = 10;
        #endregion

        #region ACTIONS
        private Action<GameItemConfig> onUpdatePlayerClothes = null;
        private Action<GAME_ITEM_SLOT_TYPE> onClearPlayerClothes = null;
        #endregion

        #region PROPERTIES
        public PlayerInventorySlotModel[] PlayerEquipmentSlots { get => playerEquipmentSlots; }
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {
            HandleInventoryInput();
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<GameItemConfig> onUpdatePlayerClothes, Action<GAME_ITEM_SLOT_TYPE> onClearPlayerClothes, Action<bool> onInventoryHolderStatus = null)
        {
            this.onUpdatePlayerClothes = onUpdatePlayerClothes;
            this.onClearPlayerClothes = onClearPlayerClothes;

            playerInventorySlots = new PlayerInventorySlotModel[playerSlots];

            for (int i = 0; i < playerInventorySlots.Length; i++)
            {
                playerInventorySlots[i] = new PlayerInventorySlotModel();
            }

            playerEquipmentSlots = new PlayerInventorySlotModel[(int)GAME_ITEM_SLOT_TYPE.NONE];

            for (int i = 0; i < playerEquipmentSlots.Length; i++)
            {
                playerEquipmentSlots[i] = new PlayerInventorySlotModel();
            }

            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.HAIR, CreateItemInstance(defaultPlayerClothes.HairConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.TORSO, CreateItemInstance(defaultPlayerClothes.TorsoConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.LEGS, CreateItemInstance(defaultPlayerClothes.LegsConfig));
            TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE.FEET, CreateItemInstance(defaultPlayerClothes.FeetConfig));

            inventoryView.Configure(playerEquipmentSlots, playerInventorySlots, onInventoryHolderStatus, HandleItemClick);
            inventoryView.SetInventoryStatus(false);
        }

        public void ConfigureInput(string toggleInventoryInputName)
        {
            this.toggleInventoryInputName = toggleInventoryInputName;
        }

        public bool TryAddItemToInventory(GameItemInstanceModel gameItemInstance)
        {
            bool canAddItem = false;
            int firstUnoccupiedIndex = 0;

            for (int i = 0; i < playerInventorySlots.Length; i++)
            {
                if (!playerInventorySlots[i].IsSlotOcuppied())
                {
                    firstUnoccupiedIndex = i;
                    canAddItem = true;
                    break;
                }
            }

            if(canAddItem)
            {
                if(playerInventorySlots[firstUnoccupiedIndex].TrySetItemToSlot(gameItemInstance))
                {
                    inventoryView.SetItemToInventorySlot(playerInventorySlots[firstUnoccupiedIndex], firstUnoccupiedIndex);
                }
                return true;
            }

            return false;
        }

        public bool TryRemoveItemFromInventory(GameItemInstanceModel gameItemInstance)
        {
            //Clear from regular inventory slot
            if (GetItemIndexInInventory(gameItemInstance, out int index))
            {
                if(playerInventorySlots[index].Equipped)
                {
                    Debug.LogWarning("Cannot remove equipped item.");
                    return false;
                }

                inventoryView.ClearInventorySlot(index);
                playerInventorySlots[index].ClearSlot();

                return true;
            }

            return false;
        }

        public void TrySetItemToEquipmentSlot(GAME_ITEM_SLOT_TYPE equipmentSlotType, GameItemInstanceModel gameItemInstance)
        {
            //if they are the same instance id, swap item
            if(GetItemFromEquipmentSlot(equipmentSlotType) != null)
            {
                if (GetItemFromEquipmentSlot(equipmentSlotType).InstanceID == gameItemInstance.InstanceID)
                {
                    //try unequip item

                    //Add equipped item to inventory
                    if (TryAddItemToInventory(GetItemFromEquipmentSlot(equipmentSlotType)))
                    {
                        //Clear slot in equipment inventory
                        inventoryView.ClearEquipmentSlot(equipmentSlotType);
                        playerEquipmentSlots[(int)equipmentSlotType].ClearSlot();
                        onClearPlayerClothes.Invoke(equipmentSlotType);
                    }
                    else
                    {
                        //Couldn't unequip item
                        Debug.LogWarning("Couldn't unequip item");
                    }

                    return;
                }
            }

            if (playerEquipmentSlots[(int)equipmentSlotType].TrySetItemToSlot(gameItemInstance, true))
            {
                //Clear from regular inventory slot
                if(GetItemIndexInInventory(gameItemInstance, out int index))
                {
                    inventoryView.ClearInventorySlot(index);
                    playerInventorySlots[index].ClearSlot();
                }

                onUpdatePlayerClothes.Invoke(gameItemInstance.ItemConfigAttached);
                inventoryView.SetItemToEquipmentSlot(playerEquipmentSlots[(int)equipmentSlotType], (int)equipmentSlotType);
            }
            else
            {
                //Slot is occupied, neet to swap
                if (GetItemIndexInInventory(gameItemInstance, out int index))
                {
                    //Clear slot in inventory, keep item instance
                    playerInventorySlots[index].ClearSlot();
                    inventoryView.ClearInventorySlot(index);

                    //Add equipped item to inventory
                    if (TryAddItemToInventory(GetItemFromEquipmentSlot(equipmentSlotType)))
                    {
                        //Clear slot in equipment inventory
                        inventoryView.ClearEquipmentSlot(equipmentSlotType);
                        playerEquipmentSlots[(int)equipmentSlotType].ClearSlot();

                        TrySetItemToEquipmentSlot(equipmentSlotType, gameItemInstance);
                    }
                    else
                    {
                        //Couldn't swap item
                        Debug.LogWarning("Couldn't swap item");
                    }
                }
            }
        }

        public GameItemInstanceModel CreateItemInstance(GameItemConfig itemConfig)
        {
            return new GameItemInstanceModel(itemConfig);
        }

        public GameItemInstanceModel[] GetValidInventoryItems()
        {
            List<GameItemInstanceModel> items = new List<GameItemInstanceModel>();

            for (int i = 0; i < playerInventorySlots.Length; i++)
            {
                if (playerInventorySlots[i].GameItemInstance != null)
                {
                    items.Add(playerInventorySlots[i].GameItemInstance);
                }
            }

            return items.ToArray();
        }

        public GameItemInstanceModel[] GetValidEquipmentItems()
        {
            List<GameItemInstanceModel> items = new List<GameItemInstanceModel>();

            for (int i = 0; i < playerEquipmentSlots.Length; i++)
            {
                if (playerEquipmentSlots[i].GameItemInstance != null)
                {
                    items.Add(playerEquipmentSlots[i].GameItemInstance);
                }
            }

            return items.ToArray();
        }
        #endregion

        #region PRIVATE_METHODS
        private void HandleItemClick(PlayerInventorySlotModel playerInventorySlotModel)
        {
            //Equip or unequipped item
            TrySetItemToEquipmentSlot(playerInventorySlotModel.GameItemInstance.ItemConfigAttached.SlotType, playerInventorySlotModel.GameItemInstance);
        }

        private void HandleInventoryInput()
        {
            if(string.IsNullOrEmpty(toggleInventoryInputName))
            {
                return;
            }

            if(Input.GetButtonDown(toggleInventoryInputName))
            {
                inventoryView.ToggleInventory();
            }
        }

        private bool GetItemIndexInInventory(GameItemInstanceModel gameItemInstance, out int index)
        {
            for (int i = 0; i < playerInventorySlots.Length; i++)
            {
                if(playerInventorySlots[i].GameItemInstance != null)
                {
                    if (playerInventorySlots[i].GameItemInstance.InstanceID == gameItemInstance.InstanceID)
                    {
                        index = i;
                        return true;
                    }
                }
            }

            index = -1;
            return false;
        }

        private GameItemInstanceModel GetItemFromEquipmentSlot(GAME_ITEM_SLOT_TYPE equipmentSlotType)
        {
            return playerEquipmentSlots[(int)equipmentSlotType].GameItemInstance;
        }
        #endregion
    }
}