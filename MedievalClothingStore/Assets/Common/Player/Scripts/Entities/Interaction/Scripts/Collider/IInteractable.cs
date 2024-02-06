using UnityEngine;

using Common.Player.Interaction.Enums;

namespace Common.Player.Interaction.Collider
{
    public interface IInteractable
    {
        #region FIELDS
        [SerializeField] public InteractionEnums.INTERACTION_OPTIONS[] interactionOptions { get; }
        #endregion

        #region METHODS
        public (InteractionEnums.INTERACTION_OPTIONS interactionOption, object parameters)[] GetInteractionOptionParameters();
        #endregion
    }
}