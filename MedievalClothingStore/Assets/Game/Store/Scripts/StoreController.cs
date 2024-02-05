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
        [SerializeField] private StoreConfig storeConfig = null; //Store config is assigned when interacted, this is temporary
        [SerializeField] private StoreView storeView = null;
        #endregion

        #region PRIVATE_FIELDS
        private PurchaseController purchaseController = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(PurchaseController purchaseController)
        {
            this.purchaseController = purchaseController;
            storeView.Configure(storeConfig, purchaseController.TryBuyItem, purchaseController.TrySellItem);
        }

        public void DisplayBuyMenu()
        {
            storeView.DisplayBuyMenu();
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