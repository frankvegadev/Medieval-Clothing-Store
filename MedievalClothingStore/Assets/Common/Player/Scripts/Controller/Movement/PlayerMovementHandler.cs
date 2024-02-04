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

        #region PUBLIC_METHODS
        public void HandleUpdate()
        {
            HandleAxisInput();
            camMovement.HandleCameraMovement();
        }

        public void HandleFixedUpdate()
        {
            HandleAxisMovement();
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
        }
        #endregion
    }
}