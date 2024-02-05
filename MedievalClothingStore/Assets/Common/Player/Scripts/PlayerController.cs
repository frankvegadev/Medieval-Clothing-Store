using UnityEngine;

using Common.Player.Controller.Movement;
using Common.Player.Controller.Inventory;
using Common.Player.View;

using Common.GameItems.Config;

using static Common.NPC.Animations.Constants.AnimationConstants;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Comp Assigment")]
        [SerializeField] private PlayerMovementHandler playerMovement = null;
        [SerializeField] private PlayerInventoryHandler playerInventory = null;
        [SerializeField] private PlayerView playerView = null;
        #endregion

        #region UNITY_CALLS
        private void Awake()
        {
            //Temporary
            Configure();
        }

        // Update is called once per frame
        private void Update()
        {
            playerMovement.HandleUpdate();
        }

        private void FixedUpdate()
        {
            playerMovement.HandleFixedUpdate();
        }

        private void LateUpdate()
        {
            playerMovement.HandleLateUpdate();
        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure()
        {
            playerView.Configure();
            playerMovement.SetupActions(HandleOnPlayerInputLeft, HandleOnPlayerInputRight, HandleOnPlayerInputUp, HandleOnPlayerInputDown, HandleOnPlayerInputStop);
            playerInventory.Configure(HandlePlayerClothesChange);

            playerView.SetAnimationState(ANIM_STATES_NPC.STAND_DOWN);
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetPlayerClothingPart(GameItemConfig itemConfig)
        {
            playerInventory.SetPlayerClothingPart(itemConfig);
        }

        private void HandlePlayerClothesChange(GameItemConfig itemConfig)
        {
            playerView.SetClothingPart(itemConfig);
        }

        private void HandleOnPlayerInputLeft()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_LEFT);
        }

        private void HandleOnPlayerInputRight()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_RIGHT);
        }

        private void HandleOnPlayerInputUp()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_UP);
        }

        private void HandleOnPlayerInputDown()
        {
            playerView.SetAnimationState(ANIM_STATES_NPC.WALK_DOWN);
        }

        private void HandleOnPlayerInputStop()
        {
            playerView.StartStopAnimation();
        }
        #endregion
    }
}