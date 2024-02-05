using UnityEngine;

using Common.GameItems.Instance;
using Common.Player;

using Common.Purchases.Config;

namespace Common.Purchases
{
    public class PurchaseController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        #endregion

        #region PRIVATE_FIELDS
        private PlayerController playerController = null;
        #endregion

        #region ACTIONS
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public bool TryBuyItem(BuyableItemConfig buyableItem)
        {
            if(playerController.GetCoins() >= buyableItem.Price)
            {
                playerController.SubstractCoins(buyableItem.Price);
                playerController.TryAddItemToInventory(buyableItem.ItemConfig);
                return true;
            }

            return false;
        }

        public bool TrySellItem(GameItemInstanceModel itemInstance)
        {
            playerController.AddCoins(itemInstance.ItemConfigAttached.SellPrice);
            playerController.TryRemoveItemFromInventory(itemInstance);

            return true;
        }
        #endregion
    }
}