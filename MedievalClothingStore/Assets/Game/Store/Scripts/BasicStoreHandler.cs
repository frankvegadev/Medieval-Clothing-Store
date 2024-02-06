using System.Collections.Generic;

using UnityEngine;

using Common.Player.Interaction.Collider;
using Common.Player.Interaction.Enums;

using Game.Store.Config;

namespace Game.Store
{
    public class BasicStoreHandler : MonoBehaviour, IInteractable
    {
        #region EXPOSED_FIELDS
        [SerializeField] private StoreConfig storeConfig = null;
        #endregion

        #region INTERFACE_FIELDS
        public InteractionEnums.INTERACTION_OPTIONS[] interactionOptions => storeConfig.InteractionOptions;
        #endregion

        #region PUBLIC_METHODS
        public (InteractionEnums.INTERACTION_OPTIONS interactionOption, object parameters)[] GetInteractionOptionParameters()
        {
            List<(InteractionEnums.INTERACTION_OPTIONS newInteractionOption, object parameters)> optionParametersList = new List<(InteractionEnums.INTERACTION_OPTIONS interactionOption, object parameters)>();

            for (int i = 0; i < interactionOptions.Length; i++)
            {
                (InteractionEnums.INTERACTION_OPTIONS interactionOption, object parameters) newParameter = (InteractionEnums.INTERACTION_OPTIONS.NONE, null);

                if (interactionOptions[i] == InteractionEnums.INTERACTION_OPTIONS.BUY_ITEMS)
                {
                    newParameter = (InteractionEnums.INTERACTION_OPTIONS.BUY_ITEMS, storeConfig);
                }
                else if(interactionOptions[i] == InteractionEnums.INTERACTION_OPTIONS.TALK)
                {
                    newParameter = (InteractionEnums.INTERACTION_OPTIONS.TALK, storeConfig);
                }
                else
                {
                    newParameter = (interactionOptions[i], null);
                }

                optionParametersList.Add(newParameter);
            }

            return optionParametersList.ToArray();
        }
        #endregion
    }
}