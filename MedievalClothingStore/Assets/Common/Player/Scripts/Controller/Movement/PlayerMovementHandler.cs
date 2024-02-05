using System;

using UnityEngine;

using Common.Player.Controller.Movement.Camera;
using Common.Player.Controller.Movement.Constants;

namespace Common.Player.Controller.Movement
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Config")]
        [SerializeField] private float moveSpeed = 0;

        [Header("Comp Assigment")]
        [SerializeField] private Rigidbody2D rigidBody = null;
        [SerializeField] private CameraMovementHandler camMovement = null;
        #endregion

        #region PRIVATE_METHODS
        private float horizontalCurrentSpeed = 0;
        private float verticalCurrentSpeed = 0;
        #endregion

        #region ACTIONS
        private Action onPlayerInputLeft = null;
        private Action onPlayerInputRight = null;
        private Action onPlayerInputUp = null;
        private Action onPlayerInputDown = null;
        private Action onPlayerInputStop = null;
        #endregion

        #region PUBLIC_METHODS
        public void HandleUpdate()
        {
            HandleAxisInput();
        }

        public void HandleFixedUpdate()
        {
            HandleAxisMovement();
        }

        public void HandleLateUpdate()
        {
            camMovement.HandleCameraMovement();
        }

        public void SetupActions(Action onPlayerInputLeft, Action onPlayerInputRight, Action onPlayerInputUp, Action onPlayerInputDown,
            Action onPlayerInputStop)
        {
            this.onPlayerInputLeft = onPlayerInputLeft;
            this.onPlayerInputRight = onPlayerInputRight;
            this.onPlayerInputStop = onPlayerInputStop;
            this.onPlayerInputUp = onPlayerInputUp;
            this.onPlayerInputDown = onPlayerInputDown;
        }
        #endregion

        #region PRIVATE_METHODS
        private void HandleAxisInput()
        {
            horizontalCurrentSpeed = Input.GetAxis(MovementConstants.movementAxisXInput);
            verticalCurrentSpeed = Input.GetAxis(MovementConstants.movementAxisYInput);
        }

        private void HandleAxisMovement()
        {
            rigidBody.velocity = new Vector2(horizontalCurrentSpeed, verticalCurrentSpeed).normalized * moveSpeed;

            if (rigidBody.velocity.y > 0)
            {
                onPlayerInputUp?.Invoke();
                return;
            }
            else if (rigidBody.velocity.y < 0)
            {
                onPlayerInputDown?.Invoke();
                return;
            }

            if (rigidBody.velocity.x > 0)
            {
                onPlayerInputRight?.Invoke();
                return;
            }
            else if(rigidBody.velocity.x < 0)
            {
                onPlayerInputLeft?.Invoke();
                return;
            }

            if(rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
            {
                onPlayerInputStop?.Invoke();
            }
        }
        #endregion
    }
}