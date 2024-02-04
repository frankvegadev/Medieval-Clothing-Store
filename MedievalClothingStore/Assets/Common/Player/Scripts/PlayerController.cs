using UnityEngine;

using Common.Player.Controller.Movement;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Comp Assigment")]
        [SerializeField] private PlayerMovementHandler playerMovement = null;
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        private void Update()
        {
            playerMovement.HandleUpdate();
        }

        private void FixedUpdate()
        {
            playerMovement.HandleFixedUpdate();
        }
        #endregion
    }
}