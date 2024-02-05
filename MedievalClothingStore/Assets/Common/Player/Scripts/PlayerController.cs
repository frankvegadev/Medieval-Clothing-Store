using UnityEngine;

using Common.Player.Controller.Movement;
using Common.Player.View;

using Common.NPC.Animations.Constants;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Comp Assigment")]
        [SerializeField] private PlayerMovementHandler playerMovement = null;
        [SerializeField] private PlayerView playerView = null;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            //Temporary
            playerMovement.SetupActions(HandleOnPlayerInputLeft, HandleOnPlayerInputRight, HandleOnPlayerInputUp, HandleOnPlayerInputDown, HandleOnPlayerInputStop);
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

        #region PRIVATE_METHODS
        private void HandleOnPlayerInputLeft()
        {
            playerView.SetAnimationState(AnimationConstants.ANIM_STATES_NPC.WALK_LEFT);
        }

        private void HandleOnPlayerInputRight()
        {
            playerView.SetAnimationState(AnimationConstants.ANIM_STATES_NPC.WALK_RIGHT);
        }

        private void HandleOnPlayerInputUp()
        {
            playerView.SetAnimationState(AnimationConstants.ANIM_STATES_NPC.WALK_UP);
        }

        private void HandleOnPlayerInputDown()
        {
            playerView.SetAnimationState(AnimationConstants.ANIM_STATES_NPC.WALK_DOWN);
        }

        private void HandleOnPlayerInputStop()
        {
            playerView.StartStopAnimation();
        }
        #endregion
    }
}