using System;

using UnityEngine;

using Common.Player.Interaction.Enums;

namespace Common.Player.Interaction.Collider
{
    public class PlayerInteractionCheck : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject interactionIcon = null;
        #endregion

        #region PRIVATE_FIELDS
        private Collider2D currentCollider = null;
        #endregion

        #region ACTIONS
        private Action<(InteractionEnums.INTERACTION_OPTIONS interactionOptions, object parameters)[]> onInteractionPossible = null;
        private Action onInteractionNotPossible = null;
        #endregion

        #region UNITY_CALLS
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IInteractable interactableInterface = collision.GetComponent<IInteractable>();

            if (interactableInterface != null)
            {
                currentCollider = collision;

                //can interact
                onInteractionPossible.Invoke(interactableInterface.GetInteractionOptionParameters());
                interactionIcon.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<IInteractable>() != null)
            {
                if(collision == currentCollider)
                {
                    //is no more interactable
                    onInteractionNotPossible.Invoke();
                    interactionIcon.SetActive(false);
                }
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<(InteractionEnums.INTERACTION_OPTIONS interactionOptions, object parameters)[]> onInteractionPossible,
            Action onInteractionNotPossible)
        {
            this.onInteractionPossible = onInteractionPossible;
            this.onInteractionNotPossible = onInteractionNotPossible;
        }
        #endregion
    }
}