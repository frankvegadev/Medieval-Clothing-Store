using UnityEngine;

namespace Common.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Config")]
        [SerializeField] private float moveSpeed = 0;

        [Header("Comp Assigment")]
        [SerializeField] private Rigidbody2D rigidBody = null;
        #endregion

        #region PRIVATE_METHODS
        private float horizontalCurrentSpeed = 0;
        private float verticalCurrentSpeed = 0;
        #endregion

        #region PUBLIC_METHODS
        public void HandleAxisInput()
        {
            horizontalCurrentSpeed = Input.GetAxis(MovementConstants.movementAxisXInput);
            verticalCurrentSpeed = Input.GetAxis(MovementConstants.movementAxisYInput);
        }

        public void HandleAxisMovement()
        {
            rigidBody.velocity = new Vector2(horizontalCurrentSpeed, verticalCurrentSpeed).normalized * moveSpeed;
        }
        #endregion
    }
}