using System;

using UnityEngine;
using UnityEngine.UI;

using Common.GameItems.Instance;
using Common.Purchases.Config;

using TMPro;

namespace Game.Store.View.Item
{
    public class StoreItemView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Image itemIcon = null;
        [SerializeField] private Image coinImage = null;
        [SerializeField] private Button buyOrSellBtn = null;
        [SerializeField] private TMP_Text titleText = null;
        [SerializeField] private TMP_Text buyOrSellText = null;
        [SerializeField] private TMP_Text priceText = null;
        #endregion

        #region CONSTANTS
        private const string buyTextString = "Buy";
        private const string sellTextString = "Sell";
        private const string soldTextString = "Sold";
        #endregion

        #region PUBLIC_METHODS
        public void ConfigureBuyView(BuyableItemConfig buyableItemConfig, Func<BuyableItemConfig, bool> onBuyItem)
        {
            itemIcon.sprite = buyableItemConfig.ItemConfig.PreviewSprite;
            titleText.text = buyableItemConfig.ItemConfig.DisplayTitle;

            buyOrSellBtn.onClick.RemoveAllListeners();
            buyOrSellBtn.onClick.AddListener(() => 
            { 
                if(onBuyItem.Invoke(buyableItemConfig))
                {
                    //Change state
                }
            });

            coinImage.gameObject.SetActive(true);

            buyOrSellText.gameObject.SetActive(true);
            buyOrSellText.text = buyTextString;

            priceText.gameObject.SetActive(true);
            priceText.text = buyableItemConfig.Price.ToString();
        }

        public void ConfigureSellView(GameItemInstanceModel sellableItemConfig, Func<GameItemInstanceModel, bool> onSellItem)
        {
            itemIcon.sprite = sellableItemConfig.ItemConfigAttached.PreviewSprite;
            titleText.text = sellableItemConfig.ItemConfigAttached.DisplayTitle;

            buyOrSellBtn.onClick.RemoveAllListeners();
            buyOrSellBtn.onClick.AddListener(() =>
            {
                if (onSellItem.Invoke(sellableItemConfig))
                {
                    //Change state
                    buyOrSellBtn.interactable = false;
                    coinImage.gameObject.SetActive(false);
                    priceText.gameObject.SetActive(false);
                    buyOrSellText.text = soldTextString;
                }
            });

            coinImage.gameObject.SetActive(true);

            buyOrSellText.gameObject.SetActive(true);
            buyOrSellText.text = sellTextString;

            priceText.gameObject.SetActive(true);
            priceText.text = sellableItemConfig.ItemConfigAttached.SellPrice.ToString();
        }
        #endregion
    }
}


