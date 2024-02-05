using UnityEngine;

using Common.Player;
using Common.GameItems.Config;
using Common.GameItems.Instance;
using Common.Purchases;
using Common.Purchases.Config;

using Game.Constants;
using Game.Store;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private PurchaseController purchaseController = null;

        [Header("Scene Config")]
        [SerializeField] private StoreController storeController = null;

        [Header("Test")]
        [SerializeField] private GameItemConfig[] testItemConfigs = null;
        [SerializeField] private BuyableItemConfig[] buyTestConfigs = null;
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
            //Temporary
            /*if(Input.GetButtonDown("Jump"))
            {
                storeController.ToggleView();
                storeController.DisplaySellMenu(playerController.GetValidInventoryItems());
            }*/
        }
        #endregion

        #region PRIVATE_METHODS
        private void InitGame()
        {
            playerController.Configure();
            purchaseController.Configure(playerController);
            storeController.Configure(purchaseController);

            playerController.ConfigureInput(InputConstants.movementAxisYInput, InputConstants.movementAxisXInput, InputConstants.toggleInventoryInput);
            GiftItemsToPlayer();

            playerController.AddCoins(550);

            TrySellGiftedItems();

            TryBuyItems();
        }

        private void GiftItemsToPlayer()
        {
            for (int i = 0; i < testItemConfigs.Length; i++)
            {
                playerController.TryAddItemToInventory(testItemConfigs[i]);
            }
        }

        private void TryBuyItems()
        {
            for (int i = 0; i < buyTestConfigs.Length; i++)
            {
                purchaseController.TryBuyItem(buyTestConfigs[i]);
            }
        }

        private void TrySellGiftedItems()
        {
            GameItemInstanceModel[] validItems = playerController.GetValidInventoryItems();

            for (int i = 0; i < validItems.Length; i++)
            {
                purchaseController.TrySellItem(validItems[i]);
            }
        }
        #endregion
    }
}