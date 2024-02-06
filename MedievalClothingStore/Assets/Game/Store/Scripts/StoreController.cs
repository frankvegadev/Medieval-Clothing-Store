using UnityEngine;

using Common.GameItems.Instance;
using Common.Purchases;

using Game.Store.Config;
using Game.Store.View;

namespace Game.Store
{
    public class StoreController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private StoreView storeView = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PurchaseController purchaseController)
        {
            storeView.Configure(purchaseController.TryBuyItem, purchaseController.TrySellItem);
        }

        public void DisplayBuyMenu(StoreConfig storeConfig)
        {
            storeView.DisplayBuyMenu(storeConfig);
        }

        public void DisplaySellMenu(GameItemInstanceModel[] sellableItems)
        {
            storeView.DisplaySellMenu(sellableItems);
        }

        public void ToggleView()
        {
            storeView.ToggleView();
        }

        public void SetViewStatus(bool status)
        {
            storeView.SetViewStatus(status);
        }
        #endregion
    }
}