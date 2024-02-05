using System;

using UnityEngine;
using UnityEngine.UI;

using Common.Player.Inventory.Model;

namespace Common.Player.Inventory.View.InventoryItemViews
{
    public class PlayerInventoryEquipmentSlotView : PlayerInventoryItemSlotView
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Image playerPreviewSlot = null;
        #endregion

        #region OVERRIDE_METHODS
        public override void Configure(PlayerInventorySlotModel model, Action<PlayerInventorySlotModel> onBtnClick)
        {
            base.Configure(model, onBtnClick);

            playerPreviewSlot.gameObject.SetActive(false);

            if (model.GameItemInstance != null)
            {
                if (model.GameItemInstance.ItemConfigAttached.AnimationConfig.StandDown.Sprite != null)
                {
                    playerPreviewSlot.sprite = model.GameItemInstance.ItemConfigAttached.AnimationConfig.StandDown.Sprite;
                    playerPreviewSlot.color = model.GameItemInstance.ItemConfigAttached.SpriteColor;
                    playerPreviewSlot.gameObject.SetActive(true);
                }
            }
        }

        public override void Clear()
        {
            base.Clear();

            playerPreviewSlot.sprite = null;
            playerPreviewSlot.color = Color.white;
            playerPreviewSlot.gameObject.SetActive(false);
        }
        #endregion
    }
}