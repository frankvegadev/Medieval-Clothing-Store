using System;

using UnityEngine;

using Common.Player.Interaction.Enums;
using Common.Player.Interaction.View.Item;

using TMPro;

namespace Common.Player.Interaction.View
{
    public class InteractionView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject holder = null;
        [SerializeField] private TMP_Text interactionText = null;

        [Header("Interaction Option Views Config")]
        [SerializeField] private InteractionOptionView cancelOptionView = null;
        [SerializeField] private InteractionOptionView buyItemsOptionView = null;
        [SerializeField] private InteractionOptionView sellItemsOptionView = null;
        [SerializeField] private InteractionOptionView talkOptionView = null;
        #endregion

        #region ACTIONS
        private Action<bool> onInteractionHolderStatus = null;
        #endregion

        #region CONSTANTS
        private const string defaultInteractionText = "What do you want to do?";
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action onPressedCancel, Action onPressedBuyItems, Action onPressedSellItems, Action onPressedTalk, Action<bool> onInteractionHolderStatus)
        {
            this.onInteractionHolderStatus = onInteractionHolderStatus;

            cancelOptionView.Configure(onPressedCancel);
            buyItemsOptionView.Configure(onPressedBuyItems);
            sellItemsOptionView.Configure(onPressedSellItems);
            talkOptionView.Configure(onPressedTalk);

            SetViewStatus(false);
        }

        public void SetInteractionOptions(InteractionEnums.INTERACTION_OPTIONS[] options)
        {
            interactionText.text = defaultInteractionText;

            SetAllInteractionOptionsStatus(false);

            for (int i = 0; i < options.Length; i++)
            {
                SetInteractionOptionViewStatus(options[i], true);
            }
        }

        public void ToggleViewStatus()
        {
            SetViewStatus(!holder.activeSelf);
        }

        public void SetViewStatus(bool status)
        {
            holder.SetActive(status);
            onInteractionHolderStatus.Invoke(status);
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetAllInteractionOptionsStatus(bool status)
        {
            cancelOptionView.gameObject.SetActive(status);
            buyItemsOptionView.gameObject.SetActive(status);
            sellItemsOptionView.gameObject.SetActive(status);
            talkOptionView.gameObject.SetActive(status);
        }

        private void SetInteractionOptionViewStatus(InteractionEnums.INTERACTION_OPTIONS option, bool status)
        {
            switch (option)
            {
                case InteractionEnums.INTERACTION_OPTIONS.CANCEL:
                    cancelOptionView.gameObject.SetActive(status);
                    break;
                case InteractionEnums.INTERACTION_OPTIONS.BUY_ITEMS:
                    buyItemsOptionView.gameObject.SetActive(status);
                    break;
                case InteractionEnums.INTERACTION_OPTIONS.SELL_ITEMS:
                    sellItemsOptionView.gameObject.SetActive(status);
                    break;
                case InteractionEnums.INTERACTION_OPTIONS.TALK:
                    talkOptionView.gameObject.SetActive(status);
                    break;
            }
        }
        #endregion
    }
}