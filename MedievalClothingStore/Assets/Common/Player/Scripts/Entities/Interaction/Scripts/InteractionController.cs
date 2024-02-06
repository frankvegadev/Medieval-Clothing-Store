using System;
using System.Collections.Generic;

using UnityEngine;

using Common.Player.Interaction.View;
using Common.Player.Interaction.Enums;
using Common.GameItems.Instance;

using Game.Store.Config;

namespace Common.Player.Interaction
{
    public class InteractionController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private InteractionView interactionView = null;
        #endregion

        #region PRIVATE_FIELDS
        private string interactInputName = string.Empty;
        private bool isInValidInteractableObject = false;

        private Dictionary<InteractionEnums.INTERACTION_OPTIONS, object> interactionOptionsParams = null;
        #endregion

        #region ACTIONS
        private Action<StoreConfig> onOpenBuyMenu = null;
        private Action<GameItemInstanceModel[]> onOpenSellMenu = null;
        private Func<GameItemInstanceModel[]> onGetInventoryItems = null;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            HandleInteractInput();
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<StoreConfig> onOpenBuyMenu, Action<GameItemInstanceModel[]> onOpenSellMenu, Func<GameItemInstanceModel[]> onGetInventoryItems,
            Action<bool> onInteractionHolderStatus)
        {
            this.onOpenBuyMenu = onOpenBuyMenu;
            this.onOpenSellMenu = onOpenSellMenu;
            this.onGetInventoryItems = onGetInventoryItems;

            interactionOptionsParams = new Dictionary<InteractionEnums.INTERACTION_OPTIONS, object>();
            interactionView.Configure(HandleCancelInteraction, HandleBuyItemsInteraction, HandleSellItemsInteraction, HandleTalkInteraction, onInteractionHolderStatus);
        }

        public void ConfigureInput(string interactInputName)
        {
            this.interactInputName = interactInputName;
        }

        public void SetupInteractionPanel((InteractionEnums.INTERACTION_OPTIONS optionsEnum, object parameters)[] interactionOptions)
        {
            // Set Current Interaction ENUM
            // handle object type param (Buyable item config, game instance,etc)
            interactionOptionsParams.Clear();
            InteractionEnums.INTERACTION_OPTIONS[] optionsEnum = new InteractionEnums.INTERACTION_OPTIONS[interactionOptions.Length];

            for (int i = 0; i < interactionOptions.Length; i++)
            {
                interactionOptionsParams.Add(interactionOptions[i].optionsEnum, interactionOptions[i].parameters);
                optionsEnum[i] = interactionOptions[i].optionsEnum;
            }

            interactionView.SetInteractionOptions(optionsEnum);

            isInValidInteractableObject = true;

            HandleOpeningDialogue();
        }

        public void ClearInteractionPanel()
        {
            isInValidInteractableObject = false;
            interactionView.SetViewStatus(false);
        }
        #endregion

        #region PRIVATE_METHODS
        private void HandleInteractInput()
        {
            if(!isInValidInteractableObject)
            {
                return;
            }

            if(Input.GetButtonDown(interactInputName))
            {
                interactionView.SetViewStatus(true);
            }
        }

        private void HandleCancelInteraction()
        {
            interactionView.SetViewStatus(false);
        }

        private void HandleBuyItemsInteraction()
        {
            StoreConfig storeConfig = interactionOptionsParams[InteractionEnums.INTERACTION_OPTIONS.BUY_ITEMS] as StoreConfig;

            if(storeConfig != null)
            {
                onOpenBuyMenu.Invoke(storeConfig);
            }

            interactionView.SetViewStatus(false);
        }

        private void HandleSellItemsInteraction()
        {
            GameItemInstanceModel[] gameInstanceModels = onGetInventoryItems.Invoke();

            if (gameInstanceModels != null)
            {
                onOpenSellMenu.Invoke(gameInstanceModels);
            }

            interactionView.SetViewStatus(false);
        }

        private void HandleTalkInteraction()
        {
            if (interactionOptionsParams.ContainsKey(InteractionEnums.INTERACTION_OPTIONS.TALK))
            {
                StoreConfig storeConfig = interactionOptionsParams[InteractionEnums.INTERACTION_OPTIONS.TALK] as StoreConfig;

                if (storeConfig != null)
                {
                    interactionView.SetInteractionDialogue(storeConfig.DialogueTalkLine, storeConfig.DialogueActorName);
                }
            }
        }

        private void HandleOpeningDialogue()
        {
            if(interactionOptionsParams.ContainsKey(InteractionEnums.INTERACTION_OPTIONS.TALK))
            {
                StoreConfig storeConfig = interactionOptionsParams[InteractionEnums.INTERACTION_OPTIONS.TALK] as StoreConfig;

                if (storeConfig != null)
                {
                    interactionView.SetInteractionDialogue(storeConfig.DialogueOpeningLine, storeConfig.DialogueActorName);
                }
            }
        }
        #endregion
    }
}