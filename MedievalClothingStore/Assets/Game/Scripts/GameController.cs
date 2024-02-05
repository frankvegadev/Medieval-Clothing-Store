using UnityEngine;

using Common.Player;

using Game.Constants;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            playerController.Configure();
            playerController.ConfigureInput(InputConstants.movementAxisYInput, InputConstants.movementAxisXInput, InputConstants.toggleInventoryInput);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}