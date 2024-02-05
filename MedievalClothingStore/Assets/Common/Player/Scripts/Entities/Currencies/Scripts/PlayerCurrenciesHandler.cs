using UnityEngine;

using Common.Player.Currencies.View;

namespace Common.Player.Currencies
{
    public class PlayerCurrenciesHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerCurrenciesView playerCurrenciesView = null;
        #endregion

        #region PRIVATE_FIELDS
        private int coinsAmount = 0;
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PUBLIC_METHODS
        public void AddCoins(int amount)
        {
            coinsAmount += amount;
            UpdateCoinsView();
        }

        public void SubstractCoins(int amount)
        {
            coinsAmount -= amount;
            UpdateCoinsView();
        }

        public void SetCoins(int amount)
        {
            coinsAmount = amount;
            UpdateCoinsView();
        }

        public int GetCoins()
        {
            return coinsAmount;
        }
        #endregion

        #region PRIVATE_METHODS
        private void UpdateCoinsView()
        {
            playerCurrenciesView.SetCoinsText(coinsAmount.ToString());
        }
        #endregion
    }
}