using UnityEngine;

using Common.NPC.Animations;

using static Common.GameItems.Constants.GameItemConstants;

namespace Common.GameItems.Config
{
    [CreateAssetMenu(fileName = "GameItemConfig_", menuName = "Game/Common/GameItems/GameItemConfig", order = 1)]
    public class GameItemConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string itemID = string.Empty;
        [SerializeField] private GAME_ITEM_SLOT_TYPE slotType = GAME_ITEM_SLOT_TYPE.NONE;
        [SerializeField] private int price = 0;
        [SerializeField] private Sprite previewSprite = null;
        [SerializeField] private NPCSpritesheetAnimationConfig animationConfig = null;
        [SerializeField] private Color spriteColor = Color.white;
        #endregion

        #region EXPOSED_FIELDS
        public string ItemID { get => itemID; }
        public GAME_ITEM_SLOT_TYPE SlotType { get => slotType; }
        public int Price { get => price; }
        public Sprite PreviewSprite { get => previewSprite; }
        public NPCSpritesheetAnimationConfig AnimationConfig { get => animationConfig; }
        public Color SpriteColor { get => spriteColor; }
        #endregion
    }
}