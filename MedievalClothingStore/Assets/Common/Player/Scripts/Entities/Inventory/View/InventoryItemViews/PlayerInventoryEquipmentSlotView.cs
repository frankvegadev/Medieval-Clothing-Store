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

        #region PUBLIC_METHODS
        public override void Configure(PlayerInventorySlotModel model)
        {
            base.Configure(model);

            playerPreviewSlot.gameObject.SetActive(false);

            if (model.GameItemInstance.ItemConfigAttached.AnimationConfig.StandDown.Sprite != null)
            {
                playerPreviewSlot.sprite = model.GameItemInstance.ItemConfigAttached.AnimationConfig.StandDown.Sprite;
                playerPreviewSlot.color = model.GameItemInstance.ItemConfigAttached.SpriteColor;
                playerPreviewSlot.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}