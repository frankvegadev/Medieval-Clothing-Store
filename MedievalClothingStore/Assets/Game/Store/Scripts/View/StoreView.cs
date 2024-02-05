using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

using Common.GameItems.Instance;
using Common.Purchases.Config;

using Game.Store.Config;
using Game.Store.View.Item;

namespace Game.Store.View
{
    public class StoreView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject holder = null;
        [SerializeField] private Transform storeItemViewsHolder = null;
        [SerializeField] private GameObject storeItemViewPrefabs = null;
        #endregion

        #region PRIVATE_FIELDS
        private StoreConfig currentStoreConfig = null;
        private ObjectPool<StoreItemView> storeItemViewPool = null;
        private List<StoreItemView> storeItemsActive = null;
        #endregion

        #region ACTIONS
        private Func<BuyableItemConfig, bool> onBuyItem = null;
        private Func<GameItemInstanceModel, bool> onSellItem = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(StoreConfig storeConfig, Func<BuyableItemConfig, bool> onBuyItem, Func<GameItemInstanceModel, bool> onSellItem)
        {
            this.currentStoreConfig = storeConfig;
            this.onSellItem = onSellItem;
            this.onBuyItem = onBuyItem;

            storeItemsActive = new List<StoreItemView>();
            storeItemViewPool = new ObjectPool<StoreItemView>(CreateStoreItemViewInstance, OnTakeStoreItemFromPool, OnReturnedStoreItemToPool, OnDestroyStoreItemPoolObject);

            SetViewStatus(false);
        }

        public void DisplayBuyMenu()
        {
            ReleaseAllActivePoolObjects();
            for (int i = 0; i < currentStoreConfig.BuyableItems.Length; i++)
            {
                StoreItemView itemView = storeItemViewPool.Get();
                storeItemsActive.Add(itemView);
                itemView.ConfigureBuyView(currentStoreConfig.BuyableItems[i], onBuyItem);
            }
        }

        public void DisplaySellMenu(GameItemInstanceModel[] sellableItems)
        {
            ReleaseAllActivePoolObjects();
            for (int i = 0; i < sellableItems.Length; i++)
            {
                StoreItemView itemView = storeItemViewPool.Get();
                storeItemsActive.Add(itemView);
                itemView.ConfigureSellView(sellableItems[i], onSellItem);
            }
        }

        public void ToggleView()
        {
            SetViewStatus(!holder.activeSelf);
        }

        public void SetViewStatus(bool status)
        {
            holder.SetActive(status);
        }
        #endregion

        #region POOL_METHODS
        private void ReleaseAllActivePoolObjects()
        {
            for (int i = 0; i < storeItemsActive.Count; i++)
            {
                storeItemViewPool.Release(storeItemsActive[i]);
            }

            storeItemsActive.Clear();
            storeItemViewPool.Clear();
        }

        private StoreItemView CreateStoreItemViewInstance()
        {
            return Instantiate(storeItemViewPrefabs, storeItemViewsHolder).GetComponent<StoreItemView>();
        }

        private void OnTakeStoreItemFromPool(StoreItemView item)
        {
            item.gameObject.SetActive(true);
        }

        private void OnReturnedStoreItemToPool(StoreItemView item)
        {
            item.gameObject.SetActive(false);
        }

        private void OnDestroyStoreItemPoolObject(StoreItemView item)
        {
            Destroy(item.gameObject);
        }
        #endregion
    }
}