using UnityEngine;

using Common.Player;
using Common.Purchases;

using Game.Constants;
using Game.Store;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private PurchaseController purchaseController = null;
        [SerializeField] private int startingCoins = 0;

        [Header("Scene Config")]
        [SerializeField] private StoreController storeController = null;
        #endregion

        #region UNITY_CALLS
        // Start is called before the first frame update
        void Start()
        {
            InitGame();
        }
        #endregion

        #region PRIVATE_METHODS
        private void InitGame()
        {
            playerController.Configure(storeController.DisplayBuyMenu, storeController.DisplaySellMenu, onCloseAllStoreMenus : () => { storeController.SetViewStatus(false); });
            purchaseController.Configure(playerController);
            storeController.Configure(purchaseController);

            playerController.ConfigureInput(InputConstants.movementAxisYInput, InputConstants.movementAxisXInput, InputConstants.toggleInventoryInput,
                InputConstants.interactInput);

            playerController.AddCoins(startingCoins);
        }
        #endregion
    }
}