using UnityEngine;

using Common.NPC.Animations.Config;

using static Common.GameItems.Constants.GameItemEnums;

namespace Common.GameItems.Config
{
    [CreateAssetMenu(fileName = "GameItemConfig_", menuName = "Game/Common/GameItems/GameItemConfig", order = 1)]
    public class GameItemConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string itemID = string.Empty;
        [SerializeField] private string displayTitle = string.Empty;
        [SerializeField] private GAME_ITEM_SLOT_TYPE slotType = GAME_ITEM_SLOT_TYPE.NONE;
        [SerializeField] private int sellPrice = 0;
        [SerializeField] private Sprite previewSprite = null;
        [SerializeField] private NPCSpritesheetAnimationConfig animationConfig = null;
        [SerializeField] private Color spriteColor = Color.white;
        #endregion

        #region EXPOSED_FIELDS
        public string ItemID { get => itemID; }
        public GAME_ITEM_SLOT_TYPE SlotType { get => slotType; }
        public int SellPrice { get => sellPrice; }
        public Sprite PreviewSprite { get => previewSprite; }
        public NPCSpritesheetAnimationConfig AnimationConfig { get => animationConfig; }
        public Color SpriteColor { get => spriteColor; }
        public string DisplayTitle { get => displayTitle; }
        #endregion
    }
}