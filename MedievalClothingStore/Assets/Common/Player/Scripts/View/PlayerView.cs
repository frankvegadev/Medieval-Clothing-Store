using UnityEngine;

using Common.GameItems.Config;
using Common.NPC.Animations;

using static Common.GameItems.Constants.GameItemConstants;
using static Common.NPC.Animations.Constants.AnimationConstants;

namespace Common.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Spritesheet Config")]
        [SerializeField] private NPCSpritesheetAnimator baseAnimator = null;
        [SerializeField] private NPCSpritesheetAnimator hairAnimator = null;
        [SerializeField] private NPCSpritesheetAnimator torsoAnimator = null;
        [SerializeField] private NPCSpritesheetAnimator legsAnimator = null;
        [SerializeField] private NPCSpritesheetAnimator feetAnimator = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure()
        {
            ConfigureNPCSpritesheetAnimators();
        }

        public void SetClothingPart(GameItemConfig item)
        {
            switch (item.SlotType)
            {
                case GAME_ITEM_SLOT_TYPE.HAIR:
                    hairAnimator.SetConfig(item.AnimationConfig, item.SpriteColor);
                    break;
                case GAME_ITEM_SLOT_TYPE.TORSO:
                    torsoAnimator.SetConfig(item.AnimationConfig, item.SpriteColor);
                    break;
                case GAME_ITEM_SLOT_TYPE.LEGS:
                    legsAnimator.SetConfig(item.AnimationConfig, item.SpriteColor);
                    break;
                case GAME_ITEM_SLOT_TYPE.FEET:
                    feetAnimator.SetConfig(item.AnimationConfig, item.SpriteColor);
                    break;
            }
        }

        public void ClearClothingPart(GAME_ITEM_SLOT_TYPE slotType)
        {
            switch (slotType)
            {
                case GAME_ITEM_SLOT_TYPE.HAIR:
                    hairAnimator.ClearConfig();
                    break;
                case GAME_ITEM_SLOT_TYPE.TORSO:
                    torsoAnimator.ClearConfig();
                    break;
                case GAME_ITEM_SLOT_TYPE.LEGS:
                    legsAnimator.ClearConfig();
                    break;
                case GAME_ITEM_SLOT_TYPE.FEET:
                    feetAnimator.ClearConfig();
                    break;
            }
        }

        public void StartStopAnimation()
        {
            baseAnimator.StartStopAnimation();
            hairAnimator.StartStopAnimation();
            torsoAnimator.StartStopAnimation();
            legsAnimator.StartStopAnimation();
            feetAnimator.StartStopAnimation();
        }

        public void SetAnimationState(ANIM_STATES_NPC newState)
        {
            if(newState == ANIM_STATES_NPC.WALK_LEFT)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            baseAnimator.ChangeAnimationState(newState);
            hairAnimator.ChangeAnimationState(newState);
            torsoAnimator.ChangeAnimationState(newState);
            legsAnimator.ChangeAnimationState(newState);
            feetAnimator.ChangeAnimationState(newState);
        }
        #endregion

        #region PRIVATE_METHODS
        private void ConfigureNPCSpritesheetAnimators()
        {
            baseAnimator.Configure();
            hairAnimator.Configure();
            torsoAnimator.Configure();
            legsAnimator.Configure();
            feetAnimator.Configure();
        }
        #endregion
    }
}