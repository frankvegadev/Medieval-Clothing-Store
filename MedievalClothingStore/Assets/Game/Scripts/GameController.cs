using UnityEngine;

using Common.Player;
using Common.GameItems.Config;

using Game.Constants;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;

        [Header("Test")]
        [SerializeField] private GameItemConfig[] testItemConfigs = null;
        #endregion

        #region UNITY_CALLS
        // Start is called before the first frame update
        void Start()
        {
            InitGame();
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PRIVATE_METHODS
        private void InitGame()
        {
            playerController.Configure();
            playerController.ConfigureInput(InputConstants.movementAxisYInput, InputConstants.movementAxisXInput, InputConstants.toggleInventoryInput);
            GiftItemsToPlayer();

            playerController.AddCoins(150);
        }

        private void GiftItemsToPlayer()
        {
            for (int i = 0; i < testItemConfigs.Length; i++)
            {
                playerController.TryAddItemToInventory(testItemConfigs[i]);
            }
        }
        #endregion
    }
}