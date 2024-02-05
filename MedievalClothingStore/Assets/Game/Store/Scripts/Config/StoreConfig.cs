using UnityEngine;

using Common.Purchases.Config;

namespace Game.Store.Config
{
    [CreateAssetMenu(fileName = "StoreConfig_", menuName = "Game/Store/StoreConfig", order = 0)]
    public class StoreConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private BuyableItemConfig[] buyableItems = null;
        [SerializeField] private bool canPlayerBuyItems = false;
        [SerializeField] private bool canPlayerSellItems = false;
        [SerializeField] private bool enableDialogue = false;
        #endregion

        #region PROPERTIES
        public BuyableItemConfig[] BuyableItems { get => buyableItems; }
        public bool CanPlayerBuyItems { get => canPlayerBuyItems; }
        public bool CanPlayerSellItems { get => canPlayerSellItems; }
        public bool EnableDialogue { get => enableDialogue; }
        #endregion
    }
}