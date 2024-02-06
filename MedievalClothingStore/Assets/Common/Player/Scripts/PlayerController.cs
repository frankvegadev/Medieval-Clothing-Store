using System;

using UnityEngine;

using Common.Player.Movement;
using Common.Player.Inventory;
using Common.Player.View;
using Common.Player.Currencies;
using Common.Player.Interaction;
using Common.Player.Interaction.Collider;

using Common.GameItems.Config;
using Common.GameItems.Instance;

using Game.Store.Config;

using static Common.NPC.Animations.Constants.AnimationConstants;
using static Common.GameItems.Constants.GameItemEnums;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Comp Assigment")]
        [SerializeField] private PlayerMovementHandler playerMovement = null;
        [SerializeField] private PlayerInventoryHandler playerInventory = null;
        [SerializeField] private PlayerCurrenciesHandler playerCurrencies = null;
        [SerializeField] private InteractionController interactionController = null;
        [SerializeField] private PlayerView playerView = null;
        [SerializeField] private PlayerInteractionCheck playerInteractionCheck = null;
        #endregion

        #region PRIVATE_FIELDS
        private bool enablePlayerMovement = true;
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        private void Update()
        {
            if(!enablePlayerMovement)
            {
                return;
            }

            playerMovement.HandleUpdate();
        }

        private void FixedUpdate()
        {
            if (!enablePlayerMovement)
            {
                return;
            }

            playerMovement.HandleFixedUpdate();
        }

        private void LateUpdate()
        {
            if (!enablePlayerMovement)
            {
                return;
            }

            playerMovement.HandleLateUpdate();
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<StoreConfig> onOpenBuyMenu, Action<GameItemInstanceModel[]> onOpenSellMenu, Action onCloseAllStoreMenus)
        {
            playerView.Configure();
            playerMovement.Configure(HandleOnPlayerInputLeft, HandleOnPlayerInputRight, HandleOnPlayerInputUp, HandleOnPlayerInputDown, HandleOnPlayerInputStop);

            playerInventory.Configure(HandlePlayerClothesChange, HandlePlayerClothesClear,
                onInventoryHolderStatus: (state) =>
                {
                    enablePlayerMovement = !state;

                    if(state)
                    {
                        playerMovement.StopPlayer();
                    }
                });

            interactionController.Configure(onOpenBuyMenu, onOpenSellMenu, GetValidInventoryItems,
                onInteractionHolderStatus: (state) =>
                {
                    playerInventory.SetInventoryInputStatus(!state);

                    if(state)
                    {
                        playerInventory.SetInventoryViewStatus(false);
                        onCloseAllStoreMenus.Invoke();
                    }
                });

            playerInteractionCheck.Configure(interactionController.SetupInteractionPanel, interactionController.ClearInteractionPanel);

            playerView.SetAnimationState(ANIM_STATES_NPC.STAND_DOWN);
        }

        public void ConfigureInput(string verticalAxisInputName, string horizontalAxisInputName, string toggleInventoryInputName, string interactInputName)
        {
            playerMovement.ConfigureInput(verticalAxisInputName, horizontalAxisInputName);
            playerInventory.ConfigureInput(toggleInventoryInputName);
            interactionController.ConfigureInput(interactInputName);
        }

        public bool TryAddItemToInventory(GameItemConfig gameItemConfig)
        {
            GameItemInstanceModel itemInstance = playerInventory.CreateItemInstance(gameItemConfig);

            return playerInventory.TryAddItemToInventory(itemInstance);
        }

        public bool TryRemoveItemFromInventory(GameItemInstanceModel gameItemInstance)
        {
            return playerInventory.TryRemoveItemFromInventory(gameItemInstance);
        }

        public void AddCoins(int amount)
        {
            playerCurrencies.AddCoins(amount);
        }

        public void SubstractCoins(int amount)
        {
            playerCurrencies.SubstractCoins(amount);
        }

        public void SetCoins(int amount)
        {
            playerCurrencies.SetCoins(amount);
        }

        public int GetCoins()
        {
            return playerCurrencies.GetCoins();
        }

        public GameItemInstanceModel[] GetValidInventoryItems()
        {
            return playerInventory.GetValidInventoryItems();
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetPlayerClothingPart(GameItemInstanceModel itemInstance)
        {
            playerInventory.TrySetItemToEquipmentSlot(itemInstance.ItemConfigAttached.SlotType, itemInstance);
        }

        private void HandlePlayerClothesChange(GameItemConfig itemConfig)
        {
            playerView.SetClothingPart(itemConfig);
        }

        private void HandlePlayerClothesClear(GAME_ITEM_SLOT_TYPE slotType)
        {
            playerView.ClearClothingPart(slotType);
        }

        private void HandleOnPlayerInputLeft()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_LEFT);
        }

        private void HandleOnPlayerInputRight()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_RIGHT);
        }

        private void HandleOnPlayerInputUp()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_UP);
        }

        private void HandleOnPlayerInputDown()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_DOWN);
        }

        private void HandleOnPlayerInputStop()
        {
            playerView.StartStopAnimation();
        }
        #endregion
    }
}