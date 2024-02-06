using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Button closeBtn = null;
        #endregion

        #region PRIVATE_FIELDS
        private ObjectPool<StoreItemView> storeItemViewPool = null;
        private List<StoreItemView> storeItemsActive = null;
        #endregion

        #region ACTIONS
        private Func<BuyableItemConfig, bool> onBuyItem = null;
        private Func<GameItemInstanceModel, bool> onSellItem = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Func<BuyableItemConfig, bool> onBuyItem, Func<GameItemInstanceModel, bool> onSellItem)
        {
            this.onSellItem = onSellItem;
            this.onBuyItem = onBuyItem;

            storeItemsActive = new List<StoreItemView>();
            storeItemViewPool = new ObjectPool<StoreItemView>(CreateStoreItemViewInstance, OnTakeStoreItemFromPool, OnReturnedStoreItemToPool, OnDestroyStoreItemPoolObject);

            closeBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.AddListener(() => { SetViewStatus(false); });

            SetViewStatus(false);
        }

        public void DisplayBuyMenu(StoreConfig storeConfig)
        {
            ReleaseAllActivePoolObjects();
            for (int i = 0; i < storeConfig.BuyableItems.Length; i++)
            {
                StoreItemView itemView = storeItemViewPool.Get();
                storeItemsActive.Add(itemView);
                itemView.ConfigureBuyView(storeConfig.BuyableItems[i], onBuyItem);
            }

            SetViewStatus(true);
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

            SetViewStatus(true);
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