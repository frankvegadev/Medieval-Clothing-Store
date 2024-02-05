using UnityEngine;
using UnityEngine.UI;

using Common.Player.Inventory.Model;

namespace Common.Player.Inventory.View.InventoryItemViews
{
    public class PlayerInventoryItemSlotView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] protected Image itemIcon = null;
        [SerializeField] protected Image equippedIcon = null;
        [SerializeField] protected Button itemBtn = null; // WIP
        #endregion

        #region PROTECTED_FIELDS
        protected PlayerInventorySlotModel modelAttached = null;
        #endregion

        #region PROPERTIES
        public PlayerInventorySlotModel ModelAttached { get => modelAttached; }
        #endregion

        #region PUBLIC_METHODS
        public virtual void Configure(PlayerInventorySlotModel model)
        {
            modelAttached = model;

            itemIcon.gameObject.SetActive(false);

            if(model.GameItemInstance != null)
            {
                if (model.GameItemInstance.ItemConfigAttached.AnimationConfig.StandDown.Sprite != null)
                {
                    itemIcon.sprite = modelAttached.GameItemInstance.ItemConfigAttached.PreviewSprite;
                    itemIcon.color = modelAttached.GameItemInstance.ItemConfigAttached.SpriteColor;
                    itemIcon.gameObject.SetActive(true);
                }
            }

            equippedIcon.gameObject.SetActive(modelAttached.Equipped);
        }

        public virtual void Clear()
        {
            modelAttached = null;
            itemIcon.sprite = null;
            itemIcon.color = Color.white;
            equippedIcon.gameObject.SetActive(false);
            itemIcon.gameObject.SetActive(false);
        }
        #endregion
    }
}