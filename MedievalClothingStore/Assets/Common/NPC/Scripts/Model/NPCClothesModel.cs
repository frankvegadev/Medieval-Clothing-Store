using UnityEngine;

using Common.GameItems.Config;

using static Common.GameItems.Constants.GameItemEnums;

namespace Common.NPC.Model
{
    [System.Serializable]
    public class NPCClothesModel
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameItemConfig hairConfig = null;
        [SerializeField] private GameItemConfig torsoConfig = null;
        [SerializeField] private GameItemConfig legsConfig = null;
        [SerializeField] private GameItemConfig feetConfig = null;
        #endregion

        #region PROPERTIES
        public GameItemConfig HairConfig { get => hairConfig;}
        public GameItemConfig TorsoConfig { get => torsoConfig; }
        public GameItemConfig LegsConfig { get => legsConfig; }
        public GameItemConfig FeetConfig { get => feetConfig; }
        #endregion

        #region PUBLIC_METHODS
        public void SetClothingPart(GameItemConfig itemPart)
        {
            switch (itemPart.SlotType)
            {
                case GAME_ITEM_SLOT_TYPE.HAIR:
                    hairConfig = itemPart;
                    break;
                case GAME_ITEM_SLOT_TYPE.TORSO:
                    torsoConfig = itemPart;
                    break;
                case GAME_ITEM_SLOT_TYPE.LEGS:
                    legsConfig = itemPart;
                    break;
                case GAME_ITEM_SLOT_TYPE.FEET:
                    feetConfig = itemPart;
                    break;
            }
        }
        #endregion
    }
}