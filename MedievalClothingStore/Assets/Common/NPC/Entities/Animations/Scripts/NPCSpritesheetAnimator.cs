using UnityEngine;

using Common.NPC.Animations.Config;

using static Common.NPC.Animations.Constants.AnimationConstants;

namespace Common.NPC.Animations
{
    public class NPCSpritesheetAnimator : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private NPCSpritesheetAnimationConfig defaultConfig = null;
        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private float defaultSpriteSwitchTime = 1;
        #endregion

        #region PRIVATE_FIELDS
        private NPCSpritesheetAnimationConfig currentConfig = null;
        private Color currentColor = Color.white;
        private ANIM_STATES_NPC currentAnimState = ANIM_STATES_NPC.NONE;

        private bool isAnimationTimerActive = false;
        private int animationIndex = 0;
        private NPCSpritesheetAnimationFrameConfig[] currentAnimSprites = null;
        private float currentAnimationTimer = 0;

        private AudioSource audioSource = null;
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {
            if(!isAnimationTimerActive)
            {
                return;
            }

            currentAnimationTimer += Time.deltaTime;

            if(currentAnimationTimer > defaultSpriteSwitchTime)
            {
                ProceedToNextAnimFrame();
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(AudioSource audioSource = null)
        {
            this.audioSource = audioSource;

            if (defaultConfig != null)
            {
                currentConfig = defaultConfig;
            }
        }

        public void StartStopAnimation()
        {
            if (currentConfig == null)
            {
                return;
            }

            ANIM_STATES_NPC newState = ANIM_STATES_NPC.NONE;

            switch (currentAnimState)
            {
                case ANIM_STATES_NPC.STAND_LEFT:
                case ANIM_STATES_NPC.STAND_RIGHT:
                    newState = ANIM_STATES_NPC.STAND_RIGHT;
                    break;
                case ANIM_STATES_NPC.STAND_UP:
                    newState = ANIM_STATES_NPC.STAND_UP;
                    break;
                case ANIM_STATES_NPC.STAND_DOWN:
                    newState = ANIM_STATES_NPC.STAND_DOWN;
                    break;
                case ANIM_STATES_NPC.WALK_LEFT:
                case ANIM_STATES_NPC.WALK_RIGHT:
                    newState = ANIM_STATES_NPC.STAND_RIGHT;
                    break;
                case ANIM_STATES_NPC.WALK_UP:
                    newState = ANIM_STATES_NPC.STAND_UP;
                    break;
                case ANIM_STATES_NPC.WALK_DOWN:
                    newState = ANIM_STATES_NPC.STAND_DOWN;
                    break;
            }

            if (newState == currentAnimState)
            {
                return;
            }

            isAnimationTimerActive = false;

            switch (currentAnimState)
            {
                case ANIM_STATES_NPC.WALK_LEFT:
                case ANIM_STATES_NPC.WALK_RIGHT:
                    SetSprite(currentConfig.StandRight, currentColor);
                    currentAnimState = ANIM_STATES_NPC.STAND_RIGHT;
                    break;
                case ANIM_STATES_NPC.WALK_UP:
                    SetSprite(currentConfig.StandUp, currentColor);
                    currentAnimState = ANIM_STATES_NPC.STAND_UP;
                    break;
                case ANIM_STATES_NPC.WALK_DOWN:
                    SetSprite(currentConfig.StandDown, currentColor);
                    currentAnimState = ANIM_STATES_NPC.STAND_DOWN;
                    break;
            }
        }

        public void ChangeAnimationState(ANIM_STATES_NPC newState)
        {
            if(currentConfig == null)
            {
                return;
            }

            if(newState == currentAnimState)
            {
                return;
            }

            currentAnimState = newState;

            isAnimationTimerActive = false;

            switch (currentAnimState)
            {
                case ANIM_STATES_NPC.STAND_LEFT:
                case ANIM_STATES_NPC.STAND_RIGHT:
                    SetSprite(currentConfig.StandRight, currentColor);
                    break;
                case ANIM_STATES_NPC.STAND_UP:
                    SetSprite(currentConfig.StandUp, currentColor);
                    break;
                case ANIM_STATES_NPC.STAND_DOWN:
                    SetSprite(currentConfig.StandDown, currentColor);
                    break;
                case ANIM_STATES_NPC.WALK_LEFT:
                case ANIM_STATES_NPC.WALK_RIGHT:
                    StartAnimation(currentConfig.WalkRight);
                    break;
                case ANIM_STATES_NPC.WALK_UP:
                    StartAnimation(currentConfig.WalkUp);
                    break;
                case ANIM_STATES_NPC.WALK_DOWN:
                    StartAnimation(currentConfig.WalkDown);
                    break;
            }
        }

        public void SetConfig(NPCSpritesheetAnimationConfig config, Color spriteColor)
        {
            currentColor = spriteColor;
            currentConfig = config;
            RefreshNPC();
        }

        public void ClearConfig()
        {
            isAnimationTimerActive = false;
            currentColor = Color.white;
            currentConfig = null;
            spriteRenderer.sprite = null;
            spriteRenderer.color = Color.white;
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetSprite(NPCSpritesheetAnimationFrameConfig frameConfig, Color spriteColor)
        {
            Sprite finalSprite = frameConfig.Sprite;

            spriteRenderer.sprite = finalSprite;
            spriteRenderer.color = spriteColor;
            spriteRenderer.flipX = frameConfig.FlipX;
            spriteRenderer.flipY = frameConfig.FlipY;

            if(audioSource != null)
            {
                if (frameConfig.AudioClipToPlay != null)
                {
                    audioSource.clip = frameConfig.AudioClipToPlay;
                    audioSource.Play();
                }
            }
        }

        private void StartAnimation(NPCSpritesheetAnimationFrameConfig[] sprites)
        {
            animationIndex = 0;
            currentAnimSprites = sprites;
            currentAnimationTimer = 0;
            isAnimationTimerActive = true;

            SetSprite(currentAnimSprites[animationIndex], currentColor);
        }

        private void ProceedToNextAnimFrame()
        {
            currentAnimationTimer = 0;

            animationIndex++;

            if(animationIndex >= currentAnimSprites.Length)
            {
                animationIndex = 0;
            }

            SetSprite(currentAnimSprites[animationIndex], currentColor);
        }

        private void RefreshNPC()
        {
            switch (currentAnimState)
            {
                case ANIM_STATES_NPC.STAND_LEFT:
                case ANIM_STATES_NPC.STAND_RIGHT:
                    SetSprite(currentConfig.StandRight, currentColor);
                    break;
                case ANIM_STATES_NPC.STAND_UP:
                    SetSprite(currentConfig.StandUp, currentColor);
                    break;
                case ANIM_STATES_NPC.STAND_DOWN:
                    SetSprite(currentConfig.StandDown, currentColor);
                    break;
                case ANIM_STATES_NPC.WALK_LEFT:
                case ANIM_STATES_NPC.WALK_RIGHT:
                    SetSprite(currentConfig.WalkRight[0], currentColor);
                    break;
                case ANIM_STATES_NPC.WALK_UP:
                    SetSprite(currentConfig.WalkUp[0], currentColor);
                    break;
                case ANIM_STATES_NPC.WALK_DOWN:
                    SetSprite(currentConfig.WalkDown[0], currentColor);
                    break;
            }
        }
        #endregion
    }
}