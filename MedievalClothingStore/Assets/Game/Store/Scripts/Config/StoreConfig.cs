using UnityEngine;

using Common.Purchases.Config;
using Common.Player.Interaction.Enums;

namespace Game.Store.Config
{
    [CreateAssetMenu(fileName = "StoreConfig_", menuName = "Game/Store/StoreConfig", order = 0)]
    public class StoreConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private BuyableItemConfig[] buyableItems = null;
        [SerializeField] private InteractionEnums.INTERACTION_OPTIONS[] interactionOptions = null;
        [SerializeField] private string dialogueActorName = string.Empty;
        [SerializeField] private string dialogueOpeningLine = string.Empty;
        [SerializeField] private string dialogueTalkLine = string.Empty;
        #endregion

        #region PROPERTIES
        public BuyableItemConfig[] BuyableItems { get => buyableItems; }
        public string DialogueOpeningLine { get => dialogueOpeningLine; }
        public InteractionEnums.INTERACTION_OPTIONS[] InteractionOptions { get => interactionOptions;  }
        public string DialogueTalkLine { get => dialogueTalkLine; }
        public string DialogueActorName { get => dialogueActorName; }
        #endregion
    }
}