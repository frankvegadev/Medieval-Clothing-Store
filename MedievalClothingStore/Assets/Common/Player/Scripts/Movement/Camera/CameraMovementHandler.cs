using UnityEngine;

namespace Common.Player.Movement.Camera
{
    public class CameraMovementHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Comp Assigment")]
        [SerializeField] private Transform playerTransform = null;
        [SerializeField] private Transform cameraTransform = null;

        [Header("Config")]
        [SerializeField] private float cameraMoveSpeed = 0;
        [SerializeField] private Vector2 cameraBoxBoundSize = default;
        #endregion

        #region PRIVATE_FIELDS
        private float cameraBoxBoundsOffsetX = 0;
        private float cameraBoxBoundsOffsetY = 0;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            cameraBoxBoundsOffsetX = cameraBoxBoundSize.x / 2;
            cameraBoxBoundsOffsetY = cameraBoxBoundSize.y / 2;
        }
        #endregion

        #region PUBLIC_METHODS
        public void HandleCameraMovement()
        {
            if (playerTransform.position.x > cameraTransform.position.x + cameraBoxBoundsOffsetX)
            {
                cameraTransform.position += new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0);
            }

            if (playerTransform.position.x < cameraTransform.position.x - cameraBoxBoundsOffsetX)
            {
                cameraTransform.position -= new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0);
            }

            if (playerTransform.position.y > cameraTransform.position.y + cameraBoxBoundsOffsetY)
            {
                cameraTransform.position += new Vector3(0, cameraMoveSpeed * Time.deltaTime, 0);
            }

            if (playerTransform.position.y < cameraTransform.position.y - cameraBoxBoundsOffsetY)
            {
                cameraTransform.position -= new Vector3(0, cameraMoveSpeed * Time.deltaTime, 0);
            }
        }
        #endregion
    }
}